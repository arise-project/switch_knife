using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxExcepts
    {
        public void Execute(string originalFile, string localeFile, string exceptFile)
        {
            var except = File.ReadAllLines(exceptFile).Select(e => e.Trim()).Where(e => !string.IsNullOrWhiteSpace(e));
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
                var e1 = except.Where(e => text1.IndexOf(e, System.StringComparison.OrdinalIgnoreCase) != -1);

                XmlNode myNode = localeRoot.SelectSingleNode($"/root/data[@name='{key}']");
                if (myNode != null)
                {
                    var text2 = myNode.ChildNodes[1].ChildNodes[0].Value;
                    var e2 = except.Where(e => text2.IndexOf(e, System.StringComparison.OrdinalIgnoreCase) != -1);

                    var diff = e1.Intersect(e2).ToArray();
                    if (diff.Length > 0)
                    {
                        char c = 'A';
                        int index = 0;
                        foreach(var e in except)
                        {
                            text1 = text1.Replace(e, c++.ToString() + index++.ToString(), StringComparison.OrdinalIgnoreCase);
                        }
                        File.AppendAllLines(Path.Combine(Path.GetDirectoryName(exceptFile), "replace.txt"), 
                            new string[] { string.Format("{0},,\"{1}\"", key, text1.Replace("\"", "\"\"")) });

                    }
                }
            }
        }
    }
}
