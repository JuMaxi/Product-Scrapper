using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IConsultEbay
    {
        public List<AccessConsult> ReturnHRefAndProduct();
    }
}
