using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IConsultTrashNothing
    {
        public List<AccessConsult> ReturnHRefAndProduct();
    }
}
