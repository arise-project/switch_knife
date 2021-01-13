using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxMissmatch
    {
        public void Execute(string folder, string originalFile, string pattern)
        {
            var files = Directory.GetFiles(folder, "*.resx", SearchOption.AllDirectories);

            XmlDocument doc = new XmlDocument();
            doc.Load(originalFile);
            XmlNode root = doc.DocumentElement;
            if(string.IsNullOrEmpty(pattern))
            {
                pattern = @"(https?|ftp|file)://[-a-zA-Z0-9+&@#/%?=~_|!:,.;]*[-a-zA-Z0-9+&@#/%=~_|]";
            }
            var urlPattern = new Regex(pattern, RegexOptions.Compiled);
            foreach (var file in files)
            {
                XmlDocument localeDoc = new XmlDocument();
                localeDoc.Load(file);
                XmlNode localeRoot = localeDoc.DocumentElement;


                XmlNodeList nodes = root.SelectNodes($"descendant::data");
                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var key = node.Attributes["name"].Value;
                    if (node.ChildNodes.Count != 3 || node.ChildNodes[1].ChildNodes.Count != 1)
                    {
                        continue;
                    }
                    var text1 = node.ChildNodes[1].ChildNodes[0].Value;
                    var url1 = urlPattern.Match(text1).Value;

                    if(!string.IsNullOrEmpty(url1))
                    {
                        XmlNode myNode = localeRoot.SelectSingleNode($"/root/data[@name='{key}']");
                        if (myNode != null)
                        {
                            var text2 = myNode.ChildNodes[1].ChildNodes[0].Value;
                            var url2 = urlPattern.Match(text2).Value;

                            if (!string.Equals(url1, url2))
                            {
                                Console.WriteLine("{0}: '{1}' but {2} - '{3}'", key, url1, Path.GetFileName(file), url2);
                                if(!string.IsNullOrEmpty(url2))
                                {
                                    myNode.ChildNodes[1].ChildNodes[0].Value = text2.Replace(url2, url1);
                                }
                            }
                        }
                    }
                }
                localeDoc.Save(file);
            }
        }
    }
}
