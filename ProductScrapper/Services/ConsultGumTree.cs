using HtmlAgilityPack;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ProductScrapper.Services
{
    public class ConsultGumTree : IConsultGumTree
    {
        private string ReadHTMLfromWebSite(string Filter)
        {
            string WebSite = "https://www.gumtree.com/search?search_category=all&q=" + Filter;
            string HTML = "";

            using (var Client = new WebClient())
            {
                HTML = Client.DownloadString(WebSite);
            }
            
            return HTML;
        }

        public List<AccessConsult> ReturnHRefAndProduct(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTMLfromWebSite(Filter));

            List<AccessConsult> AccessConsult = new List<AccessConsult>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/ul/li/article/a[@href]");

            foreach (HtmlNode Link in XPath)
            {
                string StartUrl = "https://www.gumtree.com";
                HtmlAttribute New = Link.Attributes["href"];
                AccessConsult WebSite = new AccessConsult();
                WebSite.Url = (StartUrl + New.Value);

                var H2Element = Link.SelectNodes("div/h2")[0];
                var Product = H2Element.InnerText.Trim();
                Product = Product.Replace("\r\n", " ");
                Product = Product.Replace("  ", "");
                WebSite.Product = Product;

                var Image = Link.SelectNodes("div/div/img");
                var ImageUrl = Image[0].Attributes["src"];
                WebSite.ImageProduct = ImageUrl.Value;

                AccessConsult.Add(WebSite);
            }
            return AccessConsult;
        }

    }
}
