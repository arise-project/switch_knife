using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxRepeatedChars
    {
        public void Execute(string resourceFile, int seqLength, string outputFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(resourceFile);
            XmlNode root = doc.DocumentElement;

            List<string> res = new List<string>();
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
                for (int j = seqLength; j <text1.Length - seqLength;j++)
                {
                    if(text1.Substring(j - seqLength, seqLength) == text1.Substring(j, seqLength))
                    {
                        res.Add(string.Format(
                            "{0}: {1}: {2}",
                            key,
                            text1.Substring(j - seqLength, seqLength),
                            text1));
                        break;
                    }
                }
            }

            File.AppendAllLines(outputFile, res);
        }
    }
}
