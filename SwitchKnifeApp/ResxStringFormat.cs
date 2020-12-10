using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxStringFormat
    {
        public void Execute(string inputFile, string compareFolder, string outputFile)
        {
            var files = Directory.GetFiles(compareFolder, "*.resx", SearchOption.AllDirectories);

            XmlDocument doc = new XmlDocument();
            doc.Load(inputFile);
            XmlNode root = doc.DocumentElement;
            var zero = new Regex(@"\{0\}", RegexOptions.Compiled);
            var one = new Regex(@"\{1\}", RegexOptions.Compiled);
            foreach (var file in files)
            {
                XmlDocument localeDoc = new XmlDocument();
                doc.Load(file);
                XmlNode localeRoot = doc.DocumentElement;


                XmlNodeList nodes = root.SelectNodes($"descendant::data");
                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    var key = node.Attributes["name"].Value;
                    if(node.ChildNodes.Count != 3 || node.ChildNodes[1].ChildNodes.Count != 1)
                    {
                        continue;
                    }
                    var text1 = node.ChildNodes[1].ChildNodes[0].Value;
                    var count0_1 = zero.Matches(text1).Count;
                    var count1_1 = zero.Matches(text1).Count;


                    XmlNode myNode = localeRoot.SelectSingleNode($"/root/data[@name='{key}']");
                    if(myNode != null)
                    {
                        var text2 = myNode.ChildNodes[1].ChildNodes[0].Value;
                        var count0_2 = zero.Matches(text2).Count;
                        var count1_2 = zero.Matches(text2).Count;

                        if(count0_1 != count0_2 || count1_1 != count1_2)
                        {
                            File.AppendAllLines(outputFile, new string[]
                            {
                                string.Format("{0}: {1}=>{2}\r\n{3}",
                                Path.GetFileNameWithoutExtension(file),
                                key,text2, text1)
                            });
                        }
                    }
                }
            }
        }
    }
}
