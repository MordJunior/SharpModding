using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharpModding.Demo.Core
{
    class JavaFileGenerator
    {
        private ClassDeclarationSyntax _currentClass;
        private int _openBrackets = 0;

        private bool openBrackets => _openBrackets > 0;

        private string _path;
        private List<string> lines = new List<string>();

        public JavaFileGenerator(string path)
        {
            _path = path;
        }

        public void AddClass(ClassDeclarationSyntax syntax)
        {
            if (_currentClass != null)
            {
                AddBrackets(true);
                _openBrackets--;
            }

            _currentClass = syntax;
            StringBuilder line = new StringBuilder();

            foreach (var item in syntax.Modifiers)
            {
                line.Append($"{item} ");
            }
            line.Append($"class {syntax.Identifier.ValueText}");

            if (syntax.BaseList != null)
            {
                line.Append(" : ");

                for (int i = 0; i < syntax.BaseList.Types.Count; i++)
                {
                    var item = syntax.BaseList.Types[i];
                    line.Append(item.Type);
                    if (i != syntax.BaseList.Types.Count - 1)
                        line.Append(", ");
                }

            }

            AddLine(line.ToString());
            AddBrackets(false);

            _openBrackets++;
        }

        public void AddMethod(MethodDeclarationSyntax syntax)
        {
            StringBuilder line = new StringBuilder();

            foreach (var item in syntax.Modifiers)
            {
                line.Append($"{item} ");
            }

            line.Append($"{syntax.ReturnType} ");
            line.Append(syntax.Identifier);

            AddLine(line.ToString(), false);

            AddBrackets(false);

            foreach (var item in syntax.Body.Statements)
            {
                lines.Add(item.ToString());
            }

            AddBrackets(true);
        }

        public void AddBrackets(bool close)
        {
            if (close)
                lines.Add("}");
            else
                lines.Add("{");
        }

        public void AddLine(string line , bool checkForSC = true)
        {
            if (checkForSC && !line.EndsWith(';'))
                lines.Add($"{line};");
            else
                lines.Add(line);
        }

        public void FinishFile()
        {
            if (!openBrackets)
                return;

            for (int i = 0; i < _openBrackets; i++)
                AddBrackets(true);
        }


        public async Task WriteFile() => await File.WriteAllLinesAsync(_path, lines);
    }
}
