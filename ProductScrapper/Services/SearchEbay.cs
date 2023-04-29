using HtmlAgilityPack;
using ProductScrapper.Interfaces;

namespace ProductScrapper.Services
{
    public class SearchEbay : SearchBase,ISearch
    {
        public override string GetWebSite()
        {
            string WebSite = "https://www.ebay.co.uk/sch/i.html?_from=R40&_nkw=";
            return WebSite;
        }
        public override string GetXPath()
        {
            string XPath = "//div/ul/li/div/div/a[@href]";
            return XPath;
        }
        public override string GetUrl(HtmlNode Link)
        {
            HtmlAttribute New = Link.Attributes["href"];
            return New.Value;
        }
        public override string GetProduct(HtmlNode Link) 
        {
            var SpanElement = Link.SelectNodes("div/span")[0];
            var Product = SpanElement.InnerHtml.Trim();
            Product = Product.Replace("amp;", "");

            return Product;
        }
        public override string GetImage(HtmlNode Link)
        {
            var Image = Link.SelectNodes("parent::div/parent::div/div/div/a/div/img");
            var ImageUrl = Image[0].Attributes["src"];

            return ImageUrl.Value;
        }

        
    }
}
