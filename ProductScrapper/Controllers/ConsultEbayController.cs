using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConsultEbayController : ControllerBase
    {
        IConsultEbay ConsultEbay;
        public ConsultEbayController(IConsultEbay Consult) 
        {
            ConsultEbay = Consult;
        }

        [HttpGet]
        public List<AccessConsult> ReturnListAccessConsult([FromQuery] string Filter)
        {
            List<AccessConsult> AccessConsult = ConsultEbay.ReturnHRefAndProduct(Filter);

            return AccessConsult;
        }

    }
}
