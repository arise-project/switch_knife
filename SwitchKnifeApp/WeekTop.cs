using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SwitchKnifeApp
{
    public class WeekTop
    {
        private string[] languages = new string[] { "ru", "pt", "es", "de", "da", "nl", "sv", "it", "ca", "cs", "pl", "uk", "fr", "tr", "vi", "kk", "el", "ro", "ms", "id", "zh", "zh-hant", "hi", "th", "tl", "ar", "fa", "he", "ja", "ko" };

        private string[] apps = new string[] 
        {
            "merger",
            "viewer",
            "editor",
            "parser",
            "annotation",
            "signature",
            "redaction",
            "search",
            "unlock",
            "lock",
            "conversion",
            "xfa",
            "comparison",
            "split",
            "metadata",
            "compress",
            "table-extraction",
            "page-number",
            "rotate",
            "remove",
            "form-filler",
            "ocr",
            "resize",
            "crop",
            "translate",
            "make-pdf-searchable"
        };

        private string[] formats = new string[] { "pdf", "doc", "docx", "word", "excel", "xlsx", "ppt", "powerpoint", "pptx", "tex", "html", "jpg", "png", "tiff", "words", "mobi", "bmp", "cgm", "zip", "tar", "7z", "bz2", "gz", "djvu", "rar", "xls", "csv", "xml", "ps", "xps", "epub", "latex", "mhtml", "mht", "svg", "emf", "txt", "pdfa1a", "pdfa1b", "pdfa2a", "pdfa3a", "eps", "oxps", "pcl", "md", "srt", "psd", "gif", "dcm", "dicom", "cdr", "all", "rtf", "image", "TIFF", "JPG", "PNG", "BMP", "invoice", "cv", "jp2", "j2k", "dib", "apng", "tga", "emz", "wmf", "wmz", "webp", "svgz", "dng", "odg", "otg", "cmx" };

        public void Execute(string csvFile)
        {
            Dictionary<string, int> appRate = new Dictionary<string, int>();
            Dictionary<string, int> langRate = new Dictionary<string, int>();
            Dictionary<string, int> extRate = new Dictionary<string, int>();

            var lines = File.ReadAllLines(csvFile);
            foreach(var line in lines)
            {
                var cells = line.Split('\t');
                var url = cells[2].Replace("products.aspose.app/pdf/", "");
                var count = int.Parse(cells[3].Split('(')[0].Replace(",",""));
                var segments = url.Split('/');
                string lang = "en";
                string app = "";
                
                if(languages.Any(l => l == segments[0]))
                {
                    lang = segments[0];
                    if(segments.Count() < 2)
                    {
                        continue;
                    }
                    app = segments[1];
                }
                else
                {
                    app = segments[0];
                }

                string ext1 = "pdf";
                string ext2 = "";
                if(app == "crop" || app == "resize")
                {
                    ext1 = "image";
                }

                if (app.Contains("-"))
                {
                    ext1 = formats.FirstOrDefault(f => app.IndexOf("-" + f, StringComparison.InvariantCultureIgnoreCase) != -1) ?? ext1;
                }

                if(segments.Length > 2)
                {
                    if (segments[2].Contains("-"))
                    {
                        var pair = segments[2].Split('-');
                        ext1 = formats.FirstOrDefault(f => pair[0] == f) ?? ext1;
                        if(pair.Length > 2)
                        {
                            ext2 = formats.FirstOrDefault(f => pair[2] == f) ?? ext1;
                        }
                    }
                    else
                    {
                        ext1 = segments[2];
                    }
                }

                if(!apps.Any(a => a == app))
                {
                    app = apps.FirstOrDefault(a => app.StartsWith(a)) ?? app;
                }

                if(!appRate.ContainsKey(app))
                {
                    appRate.Add(app, count);
                }
                else
                {
                    appRate[app] += count;
                }

                if (!langRate.ContainsKey(lang))
                {
                    langRate.Add(lang, count);
                }
                else
                {
                    langRate[lang] += count;
                }

                if (!extRate.ContainsKey(ext1))
                {
                    extRate.Add(ext1, count);
                }
                else
                {
                    extRate[ext1] += count;
                }

                if(!string.IsNullOrWhiteSpace(ext2))
                {
                    if (!extRate.ContainsKey(ext2))
                    {
                        extRate.Add(ext2, count);
                    }
                    else
                    {
                        extRate[ext2] += count;
                    }
                }
            }

            var sortedApps = appRate.OrderByDescending(g => g.Value);
            var sortedLang = langRate.OrderByDescending(g => g.Value);
            var sortedExt = extRate.OrderByDescending(g => g.Value);

            var emptyApps = apps.Except(appRate.Select(g => g.Key));

            foreach (var a in sortedApps)
            {
                Console.WriteLine("{0} {1}", a.Key, a.Value);
            }
            Console.WriteLine();
            foreach (var l in sortedLang)
            {
                Console.WriteLine("{0} {1}", l.Key, l.Value);
            }
            Console.WriteLine();
            foreach (var e in sortedExt)
            {
                Console.WriteLine("{0} {1}", e.Key, e.Value);
            }

            Console.WriteLine();
            foreach (var n in emptyApps)
            {
                Console.WriteLine("{0}", n);
            }
        }
    }
}
