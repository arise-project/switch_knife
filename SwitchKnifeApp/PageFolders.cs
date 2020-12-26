using System;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class PageFolders
    {
        public static readonly string[] Languages = new string[] {
            "ar",
            "de",
            "es",
            "fr",
            "hi",
            "it",
            "ja",
            "ko",
            "pl",
            "pt",
            "en",
            "ru",
            "zh"
        };

        public static Tuple<string, string, string> ParsePath(string url)
        {
            var path = url.Split('/');
            string name;
            string app;
            string lang;

            if (Languages.Contains(path[path.Length - 3]))
            {
                name = path[path.Length - 1];
                app = path[path.Length - 2];
                lang = path[path.Length - 3];
            }
            else if (Languages.Contains(path[path.Length - 2]))
            {
                name = string.Empty;
                app = path[path.Length - 1];
                lang = path[path.Length - 2];
            }
            else if (path[path.Length - 2] == "pdf")
            {
                name = string.Empty;
                app = path[path.Length - 1];
                lang = "en";
            }
            else
            {
                name = path[path.Length - 1];
                app = path[path.Length - 2];
                lang = "en";
            }

            return new Tuple<string, string, string>(lang, app, name);
        }

        public static string [] ReadList(string listFile)
        {
            return File
                .ReadAllLines(listFile)
                .Select((v, i) => new { v, i })
                .Where(g => g.v.StartsWith("https"))
                .GroupBy(g => g.v)
                .Select(g => g.OrderBy(k => k.i).First())
                .OrderBy(g => g.i)
                .Select(g => g.v)
                .ToArray();
        }

        public void Execute(string listFile, string outputFolder)
        {
            var lines = ReadList(listFile);

            foreach (var line in lines)
            {
                var parse = ParsePath(line);

                Directory.CreateDirectory(Path.Combine(outputFolder, parse.Item1));
                if(!string.IsNullOrEmpty(parse.Item3))
                {
                    Directory.CreateDirectory(Path.Combine(outputFolder, parse.Item1, parse.Item2));
                    File.Create(Path.Combine(outputFolder, parse.Item1, parse.Item2, parse.Item3) + ".html");
                }
                else 
                {
                    File.Create(Path.Combine(outputFolder, parse.Item1, parse.Item2) + ".html");
                }
            }
        }
    }
}
