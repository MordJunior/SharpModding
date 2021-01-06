using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharpModding.Demo.Converter
{
    public sealed class CSharpConverter : IDisposable
    {
        private readonly CSharpParseOptions defaultOptions = new();
        private IServiceProviderFactory<IServiceCollection> factory;
        private IServiceCollection services;
        private IServiceScope scope;

        public CSharpConverter()
        {
            factory = new DefaultServiceProviderFactory();
            services = factory.CreateBuilder(new ServiceCollection());
        }

        #region Dispose
        public void Dispose() => scope?.Dispose();
        #endregion

        #region AddServices
        public void AddServices() => AddServices<MemoryStream>(options => options.SetMinimumLevel(LogLevel.Information));

        public void AddServices(Action<ILoggingBuilder> options) => AddServices<MemoryStream>(options);

        public void AddServices<T>() where T : Stream
            => AddServices<T>(options => options.SetMinimumLevel(LogLevel.Information));

        public void AddServices<T>(Action<ILoggingBuilder> options)
            where T : Stream
        {
            services
                .AddTransient<CSharpVisitor>()
                .AddScoped(typeof(Stream), typeof(T))
                .AddScoped(typeof(Dictionary<SyntaxKind, string>), typeof(Dictionary<SyntaxKind, string>))
                .AddLogging(options);

            var provider = factory.CreateServiceProvider(services);

            scope = provider.CreateScope();
        }
        #endregion

        #region Conversion
        public async Task<Stream> ConvertFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace", nameof(path));

            var provider = scope.ServiceProvider;

            var root = await GetRootNode(path);

            var conv = provider.GetRequiredService<CSharpVisitor>();

            conv.FillReplacments();

            conv.Visit(root);

            var stream = provider.GetRequiredService<Stream>();

            stream.Position = 0;

            return stream;
        }

        private async Task<SyntaxNode> GetRootNode(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var sourceText = SourceText.From(stream, Encoding.UTF8);
                var syntaxTree = CSharpSyntaxTree.ParseText(sourceText, defaultOptions, path);
                return await syntaxTree.GetRootAsync();
            }
        } 
        #endregion
    }
}
