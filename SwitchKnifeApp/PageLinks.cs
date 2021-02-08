using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SwitchKnifeApp
{
    public class PageLinks
    {
        public void Execute(string file)
        {
            var lines = File.ReadAllLines(file);
            Regex r = new Regex(@"(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]", RegexOptions.Compiled);
            HashSet<string> list = new HashSet<string>();
            foreach(var line in lines)
            {
                var h = r.Match(line).Value;
                if(!string.IsNullOrEmpty(h) && !list.Contains(h))
                {
                    list.Add(h);
                }
            }

            foreach(var h in list)
            {
                Console.WriteLine("ADD " + h + " .");
            }
        }
    }
}
