using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace ProductScrapper.Services
{
    public class ConsultTrashNothing : IConsultTrashNothing
    {
        private string ReadHTML()
        {
            string Path = @"C:\Dev\ProductScrapper\ProductScrapper\Examples\TrashNothing.html";
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

            List<AccessConsult> AccessConsult= new List<AccessConsult>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/div/div/a[@href]");

            foreach (HtmlNode Link in XPath)
            {
                HtmlAttribute New = Link.Attributes["href"];
                AccessConsult Website = new AccessConsult();
                Website.Url = New.Value;

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
