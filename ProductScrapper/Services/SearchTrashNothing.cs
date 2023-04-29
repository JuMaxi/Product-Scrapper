using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace ProductScrapper.Services
{
    public class SearchTrashNothing : ISearch
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

        public List<Advertisement> GetAdvertisement(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTMLfromWebSite(Filter));

            List<Advertisement> Advertisements= new List<Advertisement>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/div/div/a[@href]");

            foreach (HtmlNode Link in XPath)
            {
                Advertisement Advertisement = new Advertisement();
                Advertisement.Url = GetUrl(Link);

                Advertisement.Product = GetProduct(Link);

                Advertisement.ImageProduct = GetImage(Link);

                Advertisements.Add(Advertisement);
            }
            return Advertisements;
        }
        private string GetUrl(HtmlNode Link)
        {
            string Url = "https://trashnothing.com";
            HtmlAttribute New = Link.Attributes["href"];
            Url = Url + New.Value;

            return Url;
        }
        private string GetProduct(HtmlNode Link)
        {
            var SpanElement = Link.SelectNodes("span")[0];
            var Product = SpanElement.InnerText.Trim();
            Product = Product.Replace("\n", " ");
            Product = Product.Replace("  ", "");

            return Product;
        }
        private string GetImage(HtmlNode Link)
        {
            var Image = Link.SelectNodes("div/div/div/img");

            if (Image != null)
            {
                var ImageUrl = Image[0].Attributes["src"];

                if (ImageUrl == null)
                {
                    ImageUrl = Image[0].Attributes["data-src"];
                }

                return ImageUrl.Value;
            }
            return null;
        }
    }
}

