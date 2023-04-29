using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISearch
    {
        public List<Advertisement> GetAdvertisement(string Filter);
    }
}
