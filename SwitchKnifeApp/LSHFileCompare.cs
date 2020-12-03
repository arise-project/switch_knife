using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using F23.StringSimilarity;
using System.Linq;

namespace SwitchKnifeApp
{
    public class LSHFileCompare
    {
        public class Compare 
        { 
            public string A { get; set; }
            public string B { get; set; }
            public double D { get; set; }
        }

        public void Execute(string file1, string file2)
        {
            var lines1 = File.ReadAllLines(file1);
            var lines2 = File.ReadAllLines(file2);
            var l = new Levenshtein();
            List<Compare> d = new List<Compare>();

            for (int i =0; i< lines1.Length;i++)
            {
               d.Add(new Compare { A = lines1[i], B = lines2[i], D = l.Distance(lines1[i], lines2[i]) });
            }

            d = d.OrderBy(c => c.D).ToList();

            int index = 0;
            for (int i = 0; i < lines1.Length; i++)
            {
                if(index == 10)
                {
                    Console.ReadKey();
                    index = 0;
                }
                Console.WriteLine("{0}:\r\n{1}\r\n{2}", d[i].D, d[i].B, d[i].A);
                index++;
            }
        }
    }
}
