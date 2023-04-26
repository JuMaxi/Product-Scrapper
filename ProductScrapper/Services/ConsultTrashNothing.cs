using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace ProductScrapper.Services
{
    public class ConsultTrashNothing : IConsultTrashNothing
    {
        private string ReadHTMLfromWebSite(string Filter)
        {
            string WebSite = "https://trashnothing.com/beta/browse?types=offer&search=" + Filter;
            string HTML = "";

            using(var Client = new WebClient())
            {
                HTML= Client.DownloadString(WebSite);
            }
            return HTML;
        }

        public List<AccessConsult> ReturnHRefAndProduct(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTMLfromWebSite(Filter));

            List<AccessConsult> AccessConsult= new List<AccessConsult>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/div/div/a[@href]");

            foreach (HtmlNode Link in XPath)
            {
                string StartUrl = "https://trashnothing.com";
                HtmlAttribute New = Link.Attributes["href"];
                AccessConsult Website = new AccessConsult();
                Website.Url = (StartUrl + New.Value);

                var SpanElement = Link.SelectNodes("span")[0];
                var Product = SpanElement.InnerText.Trim();
                Product = Product.Replace("\n", " ");
                Product = Product.Replace("  ", "");

                Website.Product = Product;

                AccessConsult.Add(Website);
            }
            return AccessConsult;
        }
    }
}

