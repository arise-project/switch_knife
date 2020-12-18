using System;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class RestoreExcepts
    {
        public void Execute(string file, string exceptFile)
        {
            var except = File.ReadAllLines(exceptFile).Select(e => e.Trim()).Where(e => !string.IsNullOrWhiteSpace(e)).OrderByDescending(e => e);
            var text = File.ReadAllText(file);

            int index = 0;
            foreach (var e in except)
            {
                if(text.IndexOf(index.ToString("X8")) != -1)
                {
                    text = text.Replace(index.ToString("X8"), e);
                }
                index++;
            }
            File.WriteAllText(file, text);
        }
    }
}
