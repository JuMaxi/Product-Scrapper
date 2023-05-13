using Newtonsoft.Json;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ProductScrapper.Services
{
    public class VintedProduct
    {
        public string Title { get; set; }
        public string Url { get; set; }

    }
    public class VintedResponse
    {
        public List<VintedProduct> Items { get; set; }
    }

    public class SearchVinted : ISearchVinted
    {
        
        
        public List<Advertisements> ReturnHRefAndProduct(string Filter)
        {
            string JsonApi = "https://www.vinted.co.uk/api/v2/vas_gallery/items?search_text=" + Filter;
            string Website = "https://www.vinted.co.uk/catalog?search_text=" + Filter;

            var Json = "";

            using(var J = new WebClient())
            {
                Json = J.DownloadString(JsonApi);
            }

            StreamReader Reader = new StreamReader(Json);
            var json = Reader.ReadToEnd();

            VintedResponse VintedResponse = JsonConvert.DeserializeObject<VintedResponse>(json);
            List<Advertisements> AccessConsult = new List<Advertisements>();

            foreach (VintedProduct Item in VintedResponse.Items)
            {
                Advertisements Access = new Advertisements();
                Access.Url = Item.Url;
                Access.Product = Item.Title;

                AccessConsult.Add(Access);
            }
            return AccessConsult;
        }


    }


      
}
