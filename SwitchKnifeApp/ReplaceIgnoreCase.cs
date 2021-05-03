using System;
using System.IO;

namespace SwitchKnifeApp
{
    public class ReplaceIgnoreCase
    {
        public void Execute(string folder, string from, string to)
        {
            var files = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);
            foreach(var file in files)
            {
                string text = File.ReadAllText(file);

                string toUpper = to.Substring(0, 1).ToUpper() + to.Substring(1);
                string toLower = to.Substring(0, 1).ToLower() + to.Substring(1);

                int index = -1;
                do
                {
                    index = text.IndexOf(from, 0, StringComparison.OrdinalIgnoreCase);
                    if (index != -1)
                    {
                        string found = text.Substring(index, from.Length);
                        if (found[0] >= 'a' && found[0] <= 'z')
                        {
                            text = text.Replace(found, toLower);
                        }
                        else
                        {
                            text = text.Replace(found, toUpper);
                        }
                    }
                }
                while (index != -1);

                File.WriteAllText(file, text);
            }
        }
    }
}
