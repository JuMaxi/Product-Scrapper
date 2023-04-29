using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISearchTrashNothing
    {
        public List<Advertisement> GetAdvertisement(string Filter);
    }
}
