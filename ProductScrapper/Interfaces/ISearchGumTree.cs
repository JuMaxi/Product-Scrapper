using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISearchGumTree
    {
        public List<Advertisement> GetAdvertisement(string Filter);

    }
}
