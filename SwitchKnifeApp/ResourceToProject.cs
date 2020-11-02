
using System;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class ResourceToProject
    {
        private string _projectFolder;
        private string _resourceFolder;

        public void SetArguments(string projectFolder, string resourceFolder)
        {
            _projectFolder = projectFolder;
            _resourceFolder = resourceFolder;
        }

        public void Execute()
        {
            var resources = Directory.GetFiles(_resourceFolder, "*", SearchOption.AllDirectories).Select(r => Path.GetFileName(r));

            var content = Directory.GetFiles(_projectFolder, "*.cs", SearchOption.AllDirectories).Select(f => new { f = f, c = File.ReadAllText(f) });

            var notFound = resources.Where(r => content.All(s => !s.c.Contains(r))).ToList();

            notFound.ForEach(Console.WriteLine);
        }
    }
}
