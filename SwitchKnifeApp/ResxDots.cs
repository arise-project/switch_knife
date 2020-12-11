using System.Xml;

namespace SwitchKnifeApp
{
    public class ResxDots
    {
        public void Execute(string originalFile, string localeFile)
        {
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
                var hasDot = text1.Trim().EndsWith('.');


                XmlNode myNode = localeRoot.SelectSingleNode($"/root/data[@name='{key}']");
                if (myNode != null)
                {
                    var text2 = myNode.ChildNodes[1].ChildNodes[0].Value;
                    if(!hasDot && text2.Trim().EndsWith('.'))
                    {
                        myNode.ChildNodes[1].ChildNodes[0].Value = text2.TrimEnd().TrimEnd('.');
                    }
                    else if (hasDot && !text2.Trim().EndsWith('.'))
                    {
                        myNode.ChildNodes[1].ChildNodes[0].Value = text2.TrimEnd() + '.';
                    }
                }
            }

            localeDoc.Save(localeFile);
        }
    }
}
