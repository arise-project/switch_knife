using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

namespace SwitchKnifeApp
{
    public class SitemapTest
    {
        public void Execute(string sitemapFile, int timeout)
        {
            XPathDocument document = new XPathDocument(sitemapFile);
            XPathNavigator navigator = document.CreateNavigator();
            XPathExpression query = navigator.Compile("//urlset:loc");
            XmlNamespaceManager manager = new XmlNamespaceManager(navigator.NameTable);
            manager.AddNamespace("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
            query.SetContext(manager);
            XPathNodeIterator nodes = navigator.Select(query);
            List<string> urls = new List<string>();
            foreach (XPathNavigator url in nodes)
            {
                urls.Add(url.Value);
            }

            var list = urls.ToArray();
            IWebDriver driver = new ChromeDriver();
            Random r = new Random((int)DateTime.Now.Ticks);
            while (true)
            {
                var i = r.Next(0, list.Length);
                driver.Navigate().GoToUrl(list[i]);
                Thread.Sleep(timeout);
            }
        }
    }
}
