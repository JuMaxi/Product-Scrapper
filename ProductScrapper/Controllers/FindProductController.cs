
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
        public List<Advertisements> GetAdvertisements([FromQuery] string Filter)
        {
            // 1) Extrair dados / pesquisa
            List<Advertisements> Advertisements = new List<Advertisements>();
            foreach(ISearch Search in Search)
            {
                Advertisements.AddRange(Search.GetAdvertisement(Filter));
            }
            // Fim 1)

            // 2) Gerar email

            WriteFormatEmail Write = new WriteFormatEmail();
            string HTML = Write.FormatHtml(Advertisements);
               


            // var gerador = new GeradorDeEmail()
            // string email = gerador.Gerar(Advertisements)
            // Fim 2)

            return Advertisements;
        }

    }
}
