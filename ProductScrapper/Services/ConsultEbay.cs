using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using static System.Net.WebRequestMethods;

namespace ProductScrapper.Services
{
    public class ConsultEbay : IConsultEbay
    {
        private string ReadHTMLfromWebSite(string Filter)
        {
            string WebSite = "https://www.ebay.co.uk/sch/i.html?_from=R40&_nkw=" + Filter;
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
