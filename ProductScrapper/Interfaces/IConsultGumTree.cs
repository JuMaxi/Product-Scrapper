using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IConsultGumTree
    {
        public List<AccessConsult> ReturnHRefAndProduct();

    }
}
