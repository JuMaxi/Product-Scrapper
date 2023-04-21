using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace ProductScrapper.Services
{
    public class ConsultEbay : IConsultEbay
    {
        private string ReadHTML()
        {
            string Path = @"C:\Dev\ProductScrapper\ProductScrapper\Examples\Ebay.html";
            string HTML = "";

            if(File.Exists(Path))
            {
                HTML = File.ReadAllText(Path);
            }
            return HTML;
        }

        public List<AccessConsult> ReturnHRefAndProduct()
        {
            HtmlDocument HtmlDocument = new HtmlDocument(); 
            HtmlDocument.LoadHtml(ReadHTML());

            List<AccessConsult> AccessConsult = new List<AccessConsult>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/ul/li/div/div/a[@href]");

            foreach(HtmlNode Link in XPath)
            {
                HtmlAttribute New = Link.Attributes["href"];
                AccessConsult WebSite = new AccessConsult();
                WebSite.Url = New.Value;

                var SpanElement = Link.SelectNodes("div/span")[0];
                var Product = SpanElement.InnerHtml.Trim();
                Product = Product.Replace("amp;", "");
                WebSite.Product = Product;

                AccessConsult.Add(WebSite);
            }
            return AccessConsult;
        }
    }
}
