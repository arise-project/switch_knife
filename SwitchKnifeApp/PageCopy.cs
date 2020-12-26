using System;
using System.IO;

namespace SwitchKnifeApp
{
    public class PageCopy
    {
        public void Execute(string listFile, string sourceFolder, string destFolder)
        {
            var lines = PageFolders.ReadList(listFile);

            foreach (var line in lines)
            {
                var parse = PageFolders.ParsePath(line);

                Directory.CreateDirectory(Path.Combine(destFolder, parse.Item1));
                if (!string.IsNullOrEmpty(parse.Item3))
                {
                    var sourceFile = Path.Combine(sourceFolder, parse.Item1, parse.Item2, parse.Item3 + ".html");
                    var destFile = Path.Combine(destFolder, parse.Item1, parse.Item2, parse.Item3 + ".html");

                    if (File.Exists(sourceFile))
                    {
                        Directory.CreateDirectory(Path.Combine(destFolder, parse.Item1, parse.Item2));
                        if (!File.Exists(destFile) ||
                            new FileInfo(destFile).Length < 100)
                        {
                            File.Copy(sourceFile, destFile);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Exists:" + line);
                    }
                }
                else
                {
                    var sourceFile = Path.Combine(sourceFolder, parse.Item1, parse.Item2 + ".html");
                    var destFile = Path.Combine(destFolder, parse.Item1, parse.Item2 + ".html");

                    if (File.Exists(sourceFile))
                    {
                        if (!File.Exists(destFile) ||
                            new FileInfo(destFile).Length < 100)
                        {
                            File.Copy(sourceFile, destFile);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Exists:" + line);
                    }   
                }
            }
        }
    }
}
