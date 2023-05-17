using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ICheckSendEmail
    {
        public void SaveAdvertisementDB(List<Advertisements> Advertisements);
        public List<Advertisements> CheckIfAdIsNew(List<Advertisements> Advertisements);
    }
}
