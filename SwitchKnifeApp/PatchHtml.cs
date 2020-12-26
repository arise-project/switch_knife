using System.IO;

namespace SwitchKnifeApp
{
    public class PatchHtml
    {
        public void Execute(string folder, string operation, string patch)
        {
            var files = Directory.GetFiles(folder, "*.html", SearchOption.AllDirectories);

            foreach(var file in files)
            {
                if(operation == "css")
                {
                    var text = File.ReadAllText(file);
                    text = text.Insert(text.IndexOf(@"</style>"), patch);
                    File.WriteAllText(file, text);
                }
            }
        }
    }
}
