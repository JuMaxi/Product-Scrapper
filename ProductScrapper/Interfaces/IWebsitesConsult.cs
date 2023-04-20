using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IWebsitesConsult
    {
        public List<Website> ReturnHRefAndProduct();

    }
}
