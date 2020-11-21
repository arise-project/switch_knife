using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SwitchKnifeApp.csv
{
    public class CsvValidateKeys
    {
        public void Execute(string inputFolder, int inputColumn, string outputFolder, int outputColumn)
        {
            var inputFiles = Directory.GetFiles(inputFolder, "*.csv", SearchOption.TopDirectoryOnly);
            var outputFiles = Directory.GetFiles(outputFolder, "*.csv", SearchOption.TopDirectoryOnly);

            var inputHash = new HashSet<string>();
            var outputHash = new HashSet<string>();

            foreach(var file in inputFiles)
            {
                var lines = File.ReadAllLines(file);
                var cells = lines.Select(l => Regex.Split(l, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").ToArray()).ToArray();
                var keys = cells.Select(c => c.Length > inputColumn ? c[inputColumn].Trim('"') : "-");
                foreach(var key in keys)
                {
                    if(inputHash.Contains(key))
                    {
                        Console.WriteLine("input duplicate: {0}", key);
                    }
                    else
                    {
                        inputHash.Add(key);
                    }
                }
            }

            foreach (var file in outputFiles)
            {
                var lines = File.ReadAllLines(file);
                var cells = lines.Select(l => Regex.Split(l, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").ToArray()).ToArray();
                var keys = cells.Select(c => c.Length > outputColumn ? c[outputColumn].Trim('"') : "-");
                foreach (var key in keys)
                {
                    if (outputHash.Contains(key))
                    {
                        Console.WriteLine("output duplicate: {0}", key);
                    }
                    else
                    {
                        outputHash.Add(key);
                    }
                }
            }

            foreach(var output in outputHash)
            {
                if(!inputHash.Contains(output))
                {
                    Console.WriteLine("output unknown: {0}", output);
                }
            }

            foreach (var input in inputHash)
            {
                if (!outputHash.Contains(input))
                {
                    Console.WriteLine("input unknown: {0}", input);
                }
            }

            Console.WriteLine("Total: {0} {1}", inputHash.Count, outputHash.Count);
        }
    }
}
