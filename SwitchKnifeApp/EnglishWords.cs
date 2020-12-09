using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace SwitchKnifeApp
{
    public class EnglishWords
    {
        public void Execute(string inputFolder, string outputFile)
        {
            //thank you for dict from https://github.com/dwyl/english-words
            //https://gist.github.com/deekayen/4148741

            var except = File.ReadAllLines("en_except.txt");
            HashSet<string> hash = new HashSet<string>();
            File.ReadAllLines("en_1000.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Where(l => except.All(e => !string.Equals(e.Trim(), l.Trim(), StringComparison.InvariantCultureIgnoreCase)))
                .ToList()
                .ForEach(l => hash.Add(l));
            var files = Directory.GetFiles(inputFolder, "*.resx", SearchOption.TopDirectoryOnly);

            Regex r = new Regex(@"\b\w+\b", RegexOptions.Compiled);
            foreach (var file in files)
            {
                List<KeyValuePair<string, string>> found = new List<KeyValuePair<string, string>>();
                XmlReader reader = XmlReader.Create(file);

                string key = null;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name.ToLower())
                            {
                                case "data":
                                    key = reader.GetAttribute("name");
                                    break;
                                case "value":
                                    var text = reader.ReadElementContentAsString();
                                    if (!string.IsNullOrEmpty(key))
                                    {
                                        bool done = false;
                                        var h = hash.ToArray();
                                        for (int i =0; i < hash.Count; i++)
                                        {
                                            var words =r.Matches(text).OfType<Match>().Select(m => m.Value).ToArray();
                                            for(int j = 0; j < words.Length; j++)
                                            {
                                                if (string.IsNullOrWhiteSpace(h[i]) || h[i].Length <= 3)
                                                {
                                                    continue;
                                                }

                                                if (string.Equals(words[j], h[i].Trim(), StringComparison.OrdinalIgnoreCase))
                                                {
                                                    found.Add(new KeyValuePair<string, string>(key, h[i] + "=>" + text));
                                                    done = true;
                                                    break;
                                                }
                                            }
                                            if(done)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                    if (found.Count > 0)
                    {
                        var output = outputFile + "_" + Path.GetFileName(file);
                        File.WriteAllLines(output, found.Select(pair => string.Format("{0} : {1}", pair.Key, pair.Value)).Distinct());
                    }
                }

                
            }
        }
    }
}
