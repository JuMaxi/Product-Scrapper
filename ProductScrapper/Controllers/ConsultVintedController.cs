using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Buffers.Text;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConsultVintedController : ControllerBase
    {
        IVinted IVinted;
        public ConsultVintedController(IVinted Vinted) 
        {
            IVinted= Vinted;
        }

        [HttpGet]
        public List<AccessConsult> ReturnListAccessConsult([FromQuery] string Filter)
        {
            List<AccessConsult> AccessConsult = IVinted.ReturnHRefAndProduct(Filter);

            return AccessConsult;
        }

    }
}
