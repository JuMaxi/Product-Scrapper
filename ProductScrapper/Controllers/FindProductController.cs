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
        ISearchEbay ConsultEbay;
        ISearchGumTree ConsultGumTree;
        ISearchTrashNothing ConsultTrashNothing;
        public FindProductController(ISearchEbay Ebay, ISearchGumTree GumTree, ISearchTrashNothing TrashNothing) 
        {
            ConsultEbay = Ebay;
            ConsultGumTree = GumTree;
            ConsultTrashNothing = TrashNothing;
        }

        [HttpGet]
        public List<Advertisement> GetAdvertisements([FromQuery] string Filter)
        {
            List<Advertisement> Advertisement = ConsultEbay.GetAdvertisement(Filter);

            List<Advertisement> New = ConsultGumTree.GetAdvertisement(Filter);

            Advertisement.AddRange(New);

            New = ConsultTrashNothing.GetAdvertisement(Filter);

            Advertisement.AddRange(New);    

            return Advertisement;
        }

    }
}
