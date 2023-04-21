using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class ConsultTrashNothingController : ControllerBase
    {
        IConsultTrashNothing ConsultTrashNothing;
        public ConsultTrashNothingController(IConsultTrashNothing Consult) 
        {
            ConsultTrashNothing = Consult;
        }

        [HttpGet]
        public List<AccessConsult> ReturnListAccessConsult()
        {
            List<AccessConsult> AccessConsult = ConsultTrashNothing.ReturnHRefAndProduct();

            return AccessConsult;
        }
    }
}
