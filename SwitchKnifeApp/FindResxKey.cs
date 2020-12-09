using F23.StringSimilarity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace SwitchKnifeApp
{
    public class FindResxKey
    {
        public void Execute(string inputFolder, string searchFile)
        {
            var files = Directory.GetFiles(inputFolder, "*.resx", SearchOption.TopDirectoryOnly);

            var lines = File.ReadAllLines(searchFile);
            var l = new Levenshtein();
            
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
                            switch(reader.Name.ToLower())
                            {
                                case "data":
                                    key = reader.GetAttribute("name");
                                    break;
                                case "value":
                                    var text = reader.ReadElementContentAsString();
                                    if(!string.IsNullOrEmpty(key))
                                    {                                        
                                        foreach(var line in lines)
                                        {
                                            if(string.IsNullOrWhiteSpace(line))
                                            {
                                                continue;
                                            }

                                            if(text.Contains(line.Trim()))
                                            {
                                                found.Add(new KeyValuePair<string, string>(key, text));
                                            }
                                            else
                                            {
                                                if (l.Distance(text, line) < 5)
                                                {
                                                    found.Add(new KeyValuePair<string, string>(key, text));
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }

                if(found.Count > 0)
                {
                    var output = searchFile + ".found";
                    File.AppendAllText(output, Path.GetFileName(file));
                    File.AppendAllLines(output, found.Select(pair => string.Format("{0} : {1}", pair.Key, pair.Value)));
                }
            }
        }
    }
}
