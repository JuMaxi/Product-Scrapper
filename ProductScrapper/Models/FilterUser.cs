using System.Collections.Generic;

namespace ProductScrapper.Models
{
    public class FilterUser
    {
        public string Email { get; set; }
        public List<string> Filters { get; set; }
    }
}
