using HtmlAgilityPack;
using ProductScrapper.Interfaces;

namespace ProductScrapper.Services
{
    public class SearchGumTree : SearchBase,ISearch
    {
        public override string GetWebSite()
        {
            string WebSite = "https://www.gumtree.com/search?search_category=all&q=";
            return WebSite;
        }
        public override string GetXPath()
        {
            string XPath = "//div/ul/li/article/a[@href]";
            return XPath;
        }
        public override string GetUrl(HtmlNode Link)
        {
            string Url = "https://www.gumtree.com";
            HtmlAttribute New = Link.Attributes["href"];
            Url = Url + New.Value;

            return Url;
        }
        public override string GetProduct(HtmlNode Link)
        {
            var H2Element = Link.SelectNodes("div/h2")[0];
            var Product = H2Element.InnerText.Trim();
            Product = Product.Replace("\r\n", " ");
            Product = Product.Replace("  ", "");

            return Product;
        }
        public override string GetImage(HtmlNode Link) 
        {
            var Image = Link.SelectNodes("div/div/img");
            var ImageUrl = Image[0].Attributes["src"];

            return ImageUrl.Value;
        }

    }
}
