using HtmlAgilityPack;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Website> ReturnHRefAndProduct()
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTML());

            List<Website> HRefs = new List<Website>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/ul/li/article/a[@href]");

            foreach (HtmlNode Link in XPath)
            {
                HtmlAttribute New = Link.Attributes["href"];
                Website WebSite = new Website();
                WebSite.Url = New.Value;

                var H2Element = Link.SelectNodes("div/h2")[0];
                var Product = H2Element.InnerText.Trim();
                Product = Product.Replace("\r\n", " ");
                Product = Product.Replace("  ", "");

                

                WebSite.Product = Product;

                HRefs.Add(WebSite);
            }
            return HRefs;
        }
      
    }
}
