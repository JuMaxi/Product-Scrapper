using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WebsiteController : ControllerBase
    {
        IWebsitesConsult WebsitesConsult;

        public WebsiteController(IWebsitesConsult websitesConsult)
        {
            WebsitesConsult= websitesConsult;
        }

        [HttpGet]
        public List<Website> ReturnListHRef()
        {
           List<Website> ListWebSite = WebsitesConsult.ListHRef();

            return ListWebSite;
        }

        
    }
}
