﻿using HtmlAgilityPack;
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
        public List<Advertisement> GetAdvertisement(string Filter)
        {
            HtmlDocument HtmlDocument = new HtmlDocument();
            HtmlDocument.LoadHtml(ReadHTMLfromWebSite(Filter));

            List<Advertisement> Advertisements = new List<Advertisement>();

            var XPath = HtmlDocument.DocumentNode.SelectNodes(GetXPath());

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

        public abstract string GetWebSite();
        public abstract string GetXPath();
        public abstract string GetUrl(HtmlNode Link);
        public abstract string GetProduct(HtmlNode Link);
        public abstract string GetImage(HtmlNode Link);
    }
}