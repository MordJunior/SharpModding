using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharpModding.Demo.Converter
{
    class CSharpVisitor : CSharpSyntaxWalker
    {
        private readonly ILogger<CSharpVisitor> _logger;
        private readonly IDictionary<SyntaxKind, string> _replacments;
        private readonly Stream _writer;

        public CSharpVisitor(ILogger<CSharpVisitor> logger, Stream writer) : base(SyntaxWalkerDepth.Trivia)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _replacments = new Dictionary<SyntaxKind, string>();
        }

        public void FillReplacments()
        {
            _replacments.Add(SyntaxKind.UsingKeyword, "import");
            _replacments.Add(SyntaxKind.NamespaceKeyword, "package");
        }

        private async Task Write(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            await _writer.WriteAsync(bytes.AsMemory());
        }
        private async Task Write(SyntaxToken token) => await Write(token.ValueText);
        private async Task Write(SyntaxTrivia trivia) => await Write(trivia.ToString());

        public override async void VisitToken(SyntaxToken token)
        {
            var kind = token.Kind();
            _logger.LogInformation("Token of type {0} found", kind, token);
            VisitLeadingTrivia(token);
            if (_replacments.TryGetValue(kind, out string replacment))
            {
                _logger.LogDebug("Value of token with \"{0}\" replaced", replacment);
                await Write(replacment);
            }
            else
            {
                _logger.LogDebug("Value of token unchanged");
                await Write(token);
            }
            VisitTrailingTrivia(token);
        }

        public override async void VisitTrivia(SyntaxTrivia trivia)
        {
            var kind = trivia.Kind();
            _logger.LogInformation("Trivia of type {0} found", kind, trivia);
            if (_replacments.TryGetValue(kind, out string replacment))
            {
                _logger.LogDebug("Value of trivia with \"{0}\" replaced", replacment);
                await Write(replacment);
            }
            else
            {
                _logger.LogDebug("Value of trivia unchanged");
                await Write(trivia);
            }
            base.VisitTrivia(trivia);
        }
    }
}
