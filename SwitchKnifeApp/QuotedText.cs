using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SwitchKnifeApp
{
    public class QuotedText
    {
        public void Execute(string fileName)
        {
            var text = File.ReadAllText(fileName);
            var values = new HashSet<string>();
            int start = -1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '"')
                {
                    if (start == -1)
                    {
                        start = i;
                    }
                    else
                    {
                        var value = text.Substring(start + 1, i - start - 1);
                        if (!values.Contains(value))
                        {
                            values.Add(value);
                        }
                        start = -1;
                    }
                }
            }

            foreach(var value in values)
            {
                Console.Write("\"{0}\", ", value);
            }
        }
    }
}
