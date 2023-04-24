using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IVinted
    {
        public List<AccessConsult> ReturnHRefAndProduct();
    }
}
