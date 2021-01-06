using Microsoft.Extensions.Logging;
using SharpModding.Demo.Converter;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpModding.Demo.Client
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var path = "../../../../../DemoProject/DemoConsole/Program.cs";

            var converter = new CSharpConverter();

            converter.AddServices<MemoryStream>(options => options.SetMinimumLevel(LogLevel.Debug).AddConsole());

            using (var stream = await converter.ConvertFile(path))
            using (var reader = new StreamReader(stream))
            {
                await Task.Delay(100);
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(await reader.ReadLineAsync());
                }
            }
        }
    }
}
