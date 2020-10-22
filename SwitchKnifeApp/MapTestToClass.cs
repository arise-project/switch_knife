using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SwitchKnifeApp
{
    public class MapTestToClass
    {
        private string _testsFolder;
        private string _classesFolder;

        public void SetArguments(string testsFolder, string classesFolder)
        {
            _classesFolder = classesFolder;
            _testsFolder = testsFolder;
        }

        public void Execute()
        {
            var testFiles = Directory.GetFiles(_testsFolder, "*.cs", SearchOption.AllDirectories);
            var classFiles = Directory.GetFiles(_classesFolder, "*.cs", SearchOption.AllDirectories);
            
            Regex r = new Regex(@"class \S+", RegexOptions.Compiled);
            var classes = classFiles
            .SelectMany(cf =>  r.Matches(File.ReadAllText(cf))
                                .Cast<Match>()
                                .Select(m => m.Value.Substring("class".Length).Trim()))
            .Where(c => testFiles.Any(t => Path.GetFileNameWithoutExtension(t).StartsWith(c, true, CultureInfo.InvariantCulture)))
            .ToList();
            classes.ForEach(c => Console.WriteLine(c));
        }
    }
}