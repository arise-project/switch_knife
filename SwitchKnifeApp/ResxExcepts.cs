using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxExcepts
    {
        public void Execute(string originalFile, string localeFile, string exceptFile)
        {
            var except = File.ReadAllLines(exceptFile).Select(e => e.Trim()).Where(e => !string.IsNullOrWhiteSpace(e)).OrderByDescending(e => e);
            XmlDocument doc = new XmlDocument();
            doc.Load(originalFile);
            XmlNode root = doc.DocumentElement;
            XmlDocument localeDoc = new XmlDocument();
            localeDoc.Load(localeFile);
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
                if(text1.Trim().StartsWith("https") && !text1.Trim().Contains(" "))
                {
                    continue;
                }
                var e1 = except.Where(e => text1.IndexOf(e, System.StringComparison.OrdinalIgnoreCase) != -1);

                XmlNode myNode = localeRoot.SelectSingleNode($"/root/data[@name='{key}']");
                if (myNode != null)
                {
                    var text2 = myNode.ChildNodes[1].ChildNodes[0].Value;
                    var e2 = except.Where(e => text2.IndexOf(e, System.StringComparison.OrdinalIgnoreCase) != -1);

                    var diff = e1.Intersect(e2).ToArray();
                    if (diff.Length > 0)
                    {
                        int index = 0;
                        string text3 = text1;
                        foreach(var e in except)
                        {
                            if(e != "{0}" && e != "{1}")
                            {
                                var r1 = new Regex(@"\b" + e + @"\b", RegexOptions.IgnoreCase);
                                text3 = r1.Replace(text3, index++.ToString("X8"));
                                //text3 = text3.Replace(e, index++.ToString("X8"), StringComparison.OrdinalIgnoreCase);
                            }
                            else
                            {
                                text3 = text3.Replace(e, index++.ToString("X8"));
                            }
                        }
                        if(text3 != text1)
                        {
                            File.AppendAllLines(Path.Combine(Path.GetDirectoryName(exceptFile), "replace.txt"),
                            new string[] { string.Format("{0},,\"{1}\"", key, text3.Replace("\"", "\"\"")) });
                        }
                    }
                }
            }
        }
    }
}
