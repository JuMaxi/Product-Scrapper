using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ProductScrapper.Services
{
    public class WebsitesConsult : IWebsitesConsult
    {
        private string ReadHTML()
        {
            string Path = @"C:\Dev\ProductScrapper\ProductScrapper\Examples\htmlpage.html";
            string HTML = "";

            if (File.Exists(Path))
            {
                HTML = File.ReadAllText(Path);
            }
            return HTML;
        }

        public List<Website> ListHRef()
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTML());

            List<Website> HRefs = new List<Website>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/ul/li/article/a[@href] | //a/h2");

            foreach (HtmlNode Link in XPath)
            {
                HtmlAttribute New = Link.Attributes["href"];
                Website WebSite = new Website();
                WebSite.Url = New.Value;
                HRefs.Add(WebSite);
            }
            return HRefs;
        }
    }
}
