using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SharpModding.Demo.Core
{
    class JavaFileGenerator
    {
        private string _path;
        private List<string> lines = new List<string>();

        public JavaFileGenerator(string path)
        {
            _path = path;
        }

        public void AddLine(string line, bool addCurvedBrackets)
        {
            lines.Add(line);
            if (addCurvedBrackets)
                lines.Add("{");
        }


        public async Task WriteFile() => await File.WriteAllLinesAsync(_path, lines);
    }
}
