using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;

namespace SharpModding.Demo.Core
{
    public class DemoVisitor : CSharpSyntaxWalker
    {
        private JavaFileGenerator generator;

        public DemoVisitor(string path) : base()
        {
            generator = new JavaFileGenerator(path);
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
            generator.AddLine($"import {node.Name};", false);
            base.VisitUsingDirective(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            StringBuilder line = new StringBuilder();

            foreach (var item in node.Modifiers)
            {
                line.Append($"{item} ");
            }
            line.Append($"class {node.Identifier.ValueText}");

            if (node.BaseList != null)
            {
                line.Append(" : ");

                for (int i = 0; i < node.BaseList.Types.Count; i++)
                {
                    var item = node.BaseList.Types[i];
                    line.Append(item.Type);
                    if (i != node.BaseList.Types.Count - 1)
                        line.Append(", ");
                }

            }
            generator.AddLine(line.ToString(), true);

            base.VisitClassDeclaration(node);
        }
    }
}
