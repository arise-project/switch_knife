using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwitchKnifeApp
{
    public class CsprojStructure
    {
        public void FindFilesNotPresent(string folder, string projectFile)
        {
            var project = File.ReadAllText(projectFile);
            var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories).Select(f => f.Substring(folder.Length+1));
            var notFound = files.Where(f => !project.Contains(f, StringComparison.InvariantCultureIgnoreCase)).ToList();
            notFound.ForEach(Console.WriteLine);
        }
    }
}
