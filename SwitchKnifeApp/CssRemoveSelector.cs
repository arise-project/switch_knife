using System.IO;

namespace SwitchKnifeApp
{
    public class CssRemoveSelector
    {
        public void Execute(string cssFile, string excludeFile)
        {
            var selectors = File.ReadAllLines(excludeFile);
            var style = File.ReadAllText(cssFile);
            foreach(var selector in selectors)
            {
                var start = style.IndexOf(selector + " ");
                if (start != -1)
                {
                    var end = style.IndexOf("}", start);
                    style = style.Remove(start, end - start + 1);
                }
            }

            File.WriteAllText(cssFile + ".new", style);
        }
    }
}
