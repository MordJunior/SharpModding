using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SharpModding.Demo.Core
{
    public class DemoVisitor : CSharpSyntaxWalker
    {
        private JavaFileGenerator generator;

        public DemoVisitor(string path) : base()
        {
            generator = new JavaFileGenerator(path);
        }

        public async Task Finish()
        {
            generator.FinishFile();
            await generator.WriteFile();
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            generator.AddLine($"import {node.Name}");
            base.VisitUsingDirective(node);
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            base.VisitExpressionStatement(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            generator.AddMethod(node);
            base.VisitMethodDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            generator.AddClass(node);
            base.VisitClassDeclaration(node);
        }
    }
}
