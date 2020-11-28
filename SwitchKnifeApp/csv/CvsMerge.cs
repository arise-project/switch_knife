using System.Collections.Generic;
using System.IO;

namespace SwitchKnifeApp.csv
{
    public class CvsMerge
    {
        public void Execute(string inpultFolder, string outputFile)
        {
            var files = Directory.GetFiles(inpultFolder, "*.csv", SearchOption.TopDirectoryOnly);
            var result = new List<string>();
            foreach(var file in files)
            {
                var lines = File.ReadAllLines(file);
                result.AddRange(lines);
            }

            File.WriteAllLines(outputFile, result);
        }
    }
}
