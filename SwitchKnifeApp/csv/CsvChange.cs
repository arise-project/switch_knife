using SwitchKnifeApp.csv.opration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp.csv
{
    public class CsvChange
    {
        public void Execute(string inputFolder, string outputFolder, Change change)
        {
            var files = Directory.GetFiles(inputFolder, "*.csv", SearchOption.TopDirectoryOnly);
            foreach(var file in files)
            {
                var lines = File.ReadAllLines(file);
                var cells = lines.Select(l => l.Split(',')).ToArray();
                foreach (var move in change.Moves)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if(cells[i].Length > move.ToColumn && cells[i].Length > move.FromColumn)
                        {
                            cells[i][move.ToColumn] = cells[i][move.FromColumn];
                        }
                    }
                }

                List<List<string>> partition = new List<List<string>>();
                
                partition.Add(change.Header.Names);

                for (int i = 1; i < lines.Length; i++)
                {
                    partition.Add(cells[i].Skip(change.Shrink.StartColumn).Take(change.Shrink.EndColumn - change.Shrink.StartColumn + 1).ToList());
                }

                var output = partition.Select(p => string.Join(",", p.Select(v => "\"" + v+"\"")));
                File.WriteAllLines(Path.Combine(outputFolder, Path.GetFileName(file)), output);
            }
        }
    }
}
