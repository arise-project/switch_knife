using System;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class RestoreExcepts
    {
        public void Execute(string file, string exceptFile)
        {
            var except = File.ReadAllLines(exceptFile).Select(e => e.Trim()).Where(e => !string.IsNullOrWhiteSpace(e));
            var text = File.ReadAllText(file);

            char c = 'A';
            int index = 0;
            foreach (var e in except)
            {
                text = text.Replace(c++.ToString() + index++.ToString(), e, StringComparison.OrdinalIgnoreCase);
            }
            File.WriteAllText(file, text);
        }
    }
}
