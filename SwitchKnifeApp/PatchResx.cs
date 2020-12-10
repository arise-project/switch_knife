using System.IO;
using System.Linq;
using System.Xml;

namespace SwitchKnifeApp
{
    public class PatchResx
    {
        public void Execute(string resxFile, string pathFile)
        {
            var lines = File.ReadAllLines(pathFile);

            XmlDocument doc = new XmlDocument();
            doc.Load(resxFile);
            XmlNode root = doc.DocumentElement;
            foreach(var line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var key = line.Split(":").First().Trim();
                var text = line.Substring(line.IndexOf("=>") + 2);
                XmlNode myNode = root.SelectSingleNode($"/root/data[@name='{key}']");
                myNode.ChildNodes[1].ChildNodes[0].Value = text;
            }

            doc.Save(resxFile);
        }
    }
}
