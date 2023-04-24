using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.IO;

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
    public class ConsultVinted : IVinted
    {
        string Path = @"C:\Dev\ProductScrapper\ProductScrapper\Examples\Vinted.json";

        public List<AccessConsult> ReturnHRefAndProduct()
        {
            StreamReader Reader = new StreamReader(Path);
            var json = Reader.ReadToEnd();

            VintedResponse VintedResponse = JsonConvert.DeserializeObject<VintedResponse>(json);
            List<AccessConsult> AccessConsult = new List<AccessConsult>();

            foreach (VintedProduct Item in VintedResponse.Items)
            {
                AccessConsult Access = new AccessConsult();
                Access.Url = Item.Url;
                Access.Product = Item.Title;

                AccessConsult.Add(Access);
            }
            return AccessConsult;
        }


    }


      
}
