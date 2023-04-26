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
        IConsultEbay ConsultEbay;
        IConsultGumTree ConsultGumTree;
        IConsultTrashNothing ConsultTrashNothing;
        public FindProductController(IConsultEbay Ebay, IConsultGumTree GumTree, IConsultTrashNothing TrashNothing) 
        {
            ConsultEbay = Ebay;
            ConsultGumTree = GumTree;
            ConsultTrashNothing = TrashNothing;
        }

        [HttpGet]
        public List<AccessConsult> ReturnListAccessConsult([FromQuery] string Filter)
        {
            List<AccessConsult> AccessConsult = ConsultEbay.ReturnHRefAndProduct(Filter);

            List<AccessConsult> New = ConsultGumTree.ReturnHRefAndProduct(Filter);

            foreach(AccessConsult Line in New)
            {
                AccessConsult.Add(Line);
            }

            New = ConsultTrashNothing.ReturnHRefAndProduct(Filter);

            foreach(AccessConsult Line in New)
            {
                AccessConsult.Add(Line);
            }

            return AccessConsult;
        }

    }
}
