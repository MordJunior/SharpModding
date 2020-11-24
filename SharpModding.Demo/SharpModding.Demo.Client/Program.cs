using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SharpModding.Demo.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SharpModding.Demo.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var vis = new DemoVisitor("./javaFile.java");

            var path = @"C:\Users\levi6\Documents\GitHub\SharpModding\DemoProject\DemoConsole\Program.cs";

            var content = File.ReadAllText(path);

            var options = new CSharpParseOptions(LanguageVersion.CSharp8, DocumentationMode.None, SourceCodeKind.Regular);

            var syntaxTree = CSharpSyntaxTree.ParseText(content, options).WithFilePath(path);

            vis.Visit(syntaxTree.GetRoot());

            await vis.Finish();
        }
    }
}
