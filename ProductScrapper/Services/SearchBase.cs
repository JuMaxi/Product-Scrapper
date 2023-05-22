using HtmlAgilityPack;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.Net;

namespace ProductScrapper.Services
{
    public abstract class SearchBase
    {
        private string ReadHTMLfromWebSite(string Filter)
        {
            string WebSite = GetWebSite() + Filter;
            
            string HTML = "";

            using (var Client = new WebClient())
            {
                HTML = Client.DownloadString(WebSite);
            }
            return HTML;
        }
        public List<Advertisements> GetAdvertisement(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            var html = ReadHTMLfromWebSite(Filter);
            HtmlDocument.LoadHtml(html);

            List<Advertisements> Advertisements = new List<Advertisements>();

            var selector = GetXPath();
            var XPath = HtmlDocument.DocumentNode.SelectNodes(selector);
            if (XPath is null)
            {
                return new List<Advertisements>();
            }

            foreach (HtmlNode Link in XPath)
            {
                Advertisements Advertisement = new Advertisements();
                Advertisement.Url = GetUrl(Link);

                string Product = GetProduct(Link);
                Advertisement.Product = DropSpecialCharacter(Product);

                Advertisement.ImageProduct = GetImage(Link);

                Advertisements.Add(Advertisement);
            }
            return Advertisements;
        }

        private string DropSpecialCharacter(string Product)
        {
            for (int Position = 0; Position < Product.Length; Position++)
            {
                string SpecialCharacter = "'";
                if (Product[Position] == SpecialCharacter[0])
                {
                    Product = Product.Replace("'", " ");
                }
            }
            return Product;
        }

        public abstract string GetWebSite();
        public abstract string GetXPath();
        public abstract string GetUrl(HtmlNode Link);
        public abstract string GetProduct(HtmlNode Link);
        public abstract string GetImage(HtmlNode Link);
    }
}
