using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IRegistryFilterUser
    {
        public void RegistryEmailUser(FilterUser New);
        public void RegistryFilterToUser(FilterUser New);
        public List<FilterUser> ReadFiltersDB();
    }
}
