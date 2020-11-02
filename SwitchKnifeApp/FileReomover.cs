using System;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class FileReomover
    {
        private string _resourceFolder;
        private string _removeFile;

        public void SetArguments(string removeFile, string resourceFolder)
        {
            _removeFile = removeFile;
            _resourceFolder = resourceFolder;
        }

        public void Execute()
        {
            var list = File.ReadAllLines(_removeFile);
            Directory.GetFiles(_resourceFolder, "*", SearchOption.AllDirectories).Where(r => list.Contains(Path.GetFileName(r))).ToList().ForEach(File.Delete);
        }
    }
}
