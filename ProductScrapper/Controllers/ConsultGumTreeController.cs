using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConsultGumTreeController : ControllerBase
    {
        IConsultGumTree ConsultGumTree;

        public ConsultGumTreeController(IConsultGumTree Consult)
        {
            ConsultGumTree= Consult;
        }

        [HttpGet]
        public List<AccessConsult> ReturnListAccessConsult()
        {
           List<AccessConsult> ListWebSite = ConsultGumTree.ReturnHRefAndProduct();

            return ListWebSite;
        }

        
    }
}
