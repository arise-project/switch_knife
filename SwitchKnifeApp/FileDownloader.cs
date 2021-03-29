using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;

namespace SwitchKnifeApp
{
    public class FileDownloader
    {
        public void Execute(string url)
        {
            var reader = new UriBuilder(url);
            if (reader.Host.Contains("drive.google.com") && reader.Path.Contains("file/d/"))
            {
                var s = reader.Scheme;
                var h = reader.Host;
                var p = reader.Path.Split('/').Skip(3).FirstOrDefault();
                if(!string.IsNullOrEmpty(p))
                {
                    var builder = new UriBuilder();
                    builder.Scheme = s;
                    builder.Host = h;
                    builder.Path = "uc";
                    builder.Query = "export=download&id=" + p;
                    url = builder.Uri.AbsoluteUri;
                }
            }
            else if (reader.Host.Contains("dropbox.com"))
            {
                reader.Query = "dl=1";
                url = reader.Uri.AbsoluteUri;
            }

            var net = new WebClient();
            var data = net.DownloadData(url);
            
            string header = net.ResponseHeaders["Content-Disposition"];
            string filename;
            if (header != null)
            {
                ContentDisposition contentDisposition = new ContentDisposition(header);
                filename = contentDisposition.FileName;
            }
            else
            {
                filename = Guid.NewGuid().ToString() + ".html";
            }

            var content = new MemoryStream(data);
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                content.WriteTo(fs);
                fs.Flush();
                fs.Close();
            }
        }
    }
}
