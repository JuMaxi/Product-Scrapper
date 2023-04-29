using HtmlAgilityPack;
using ProductScrapper.Interfaces;

namespace ProductScrapper.Services
{
    public class SearchTrashNothing : SearchBase, ISearch
    {
        public override string GetWebSite()
        {
            string WebSite = "https://trashnothing.com/beta/browse?types=offer&search=";
            return WebSite;
        }
        public override string GetXPath()
        {
            string XPath = "//div/div/div/a[@href]";
            return XPath;
        }
        public override string GetUrl(HtmlNode Link)
        {
            string Url = "https://trashnothing.com";
            HtmlAttribute New = Link.Attributes["href"];
            Url = Url + New.Value;

            return Url;
        }
        public override string GetProduct(HtmlNode Link)
        {
            var SpanElement = Link.SelectNodes("span")[0];
            var Product = SpanElement.InnerText.Trim();
            Product = Product.Replace("\n", " ");
            Product = Product.Replace("  ", "");

            return Product;
        }
        public override string GetImage(HtmlNode Link)
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

