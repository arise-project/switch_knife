using System.IO;

namespace SwitchKnifeApp
{
    public class CsvAddCol
    {
        public void Execute(string csvFile, string colFile)
        {
            var rows = File.ReadAllLines(csvFile);
            var cells = File.ReadAllLines(colFile);

            for(int i = 0; i < rows.Length; i++)
            {
                if(cells.Length > i)
                {
                    rows[i] = rows[i] + ",\"" + cells[i].Replace("\"", "\"\"") + "\"";
                }
            }

            File.WriteAllLines(csvFile, rows);
        }
    }
}
