using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISearchEbay
    {
        public List<Advertisement> GetAdvertisement(string Filter);
    }
}
