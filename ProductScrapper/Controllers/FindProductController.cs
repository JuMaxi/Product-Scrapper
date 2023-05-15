
using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using ProductScrapper.Services;
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
        public string GetAdvertisements([FromQuery] string Filter)
        {
            List<Advertisements> Advertisements = new List<Advertisements>();
            foreach(ISearch Search in Search)
            {
                Advertisements.AddRange(Search.GetAdvertisement(Filter));
            }

            WriteFormatEmail Write = new WriteFormatEmail();
            string HTML = Write.FormatHtml(Advertisements);

            return HTML;
        }

    }
}
