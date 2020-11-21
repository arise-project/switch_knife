using System.IO;
using System.Linq;

namespace SwitchKnifeApp.csv
{
    public class CsvSplitter
    {
        public void Execute(string fileName, string outputFolder, int limit)
        {
            var lines = File.ReadAllLines(fileName).AsEnumerable();
            var header = lines.First();
            lines = lines.Skip(1);
            for (var i = 0; i < lines.Count() / limit + 1; i++)
            {
                File.WriteAllLines(Path.Combine(outputFolder, i + ".csv"), new string[] { header }.Union(lines.Skip(i * limit).Take(limit)));
            }
        }
    }
}
