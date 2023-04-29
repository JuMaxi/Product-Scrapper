using HtmlAgilityPack;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using static System.Net.WebRequestMethods;

namespace ProductScrapper.Services
{
    public class SearchEbay : ISearchEbay
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

        public List<Advertisement> GetAdvertisement(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument(); 
            HtmlDocument.LoadHtml(ReadHTMLfromWebSite(Filter));

            List<Advertisement> Advertisements = new List<Advertisement>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes("//div/ul/li/div/div/a[@href]");

            foreach(HtmlNode Link in XPath)
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
            HtmlAttribute New = Link.Attributes["href"];
            return New.Value;
        }
        private string GetProduct(HtmlNode Link) 
        {
            var SpanElement = Link.SelectNodes("div/span")[0];
            var Product = SpanElement.InnerHtml.Trim();
            Product = Product.Replace("amp;", "");

            return Product;
        }
        private string GetImage(HtmlNode Link)
        {
            var Image = Link.SelectNodes("parent::div/parent::div/div/div/a/div/img");
            var ImageUrl = Image[0].Attributes["src"];

            return ImageUrl.Value;
        }

    }
}
