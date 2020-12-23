using ExCSS;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Linq;

namespace SwitchKnifeApp
{
    public class CssSelectors
    {
        public enum Options
        {
            All,
            Used,
            Find
        }

        public void Execute(string htmlFile, string optionsStr)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(htmlFile);
            var parser = new StylesheetParser();
            Options options = Options.All;
            string node = null;
            if(optionsStr == "all")
            {
                options = Options.All;
            }
            else if (optionsStr == "used")
            {
                options = Options.Used;
            }
            if (optionsStr.StartsWith("find "))
            {
                options = Options.Find;
                node = optionsStr.Substring("find ".Length);
            }

            foreach (var data in doc.DocumentNode.Descendants("style"))
            {
                var stylesheet = parser.Parse(data.InnerHtml);
                foreach (var item in stylesheet.StyleRules)
                {
                    var styleRule = item as StyleRule;
                    if(options == Options.All)
                    {
                        Console.WriteLine(styleRule.Selector.Text);
                    }
                    else if (options == Options.Used)
                    {
                        try
                        {
                            if (doc.DocumentNode.QuerySelector(styleRule.Selector.Text) != null)
                            {
                                Console.WriteLine(styleRule.Selector.Text);
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(styleRule.Selector.Text + " : " + ex.Message);
                        }
                    }
                    else if (options == Options.Find)
                    {
                        try
                        {
                            var nodes = doc.DocumentNode.QuerySelectorAll(styleRule.Selector.Text);
                            if (nodes.Any(n => n.OuterHtml.Contains(node)))
                            {
                                Console.WriteLine(styleRule.Selector.Text);
                            }
                        }
                        catch (Exception)
                        {   
                        }
                    }
                }
            }
        }
    }
}
