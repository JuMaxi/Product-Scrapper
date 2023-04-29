using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FindProductController : ControllerBase
    {
        IEnumerable<ISearch> Search;
        public FindProductController(IEnumerable<ISearch> Searches) 
        {
            Search = Searches;
        }

        [HttpGet]
        public List<Advertisement> GetAdvertisements([FromQuery] string Filter)
        {
            List<Advertisement> Advertisements = new List<Advertisement>();
            foreach(ISearch Search in Search)
            {
                Advertisements.AddRange(Search.GetAdvertisement(Filter));
            }
            return Advertisements;
        }

    }
}
