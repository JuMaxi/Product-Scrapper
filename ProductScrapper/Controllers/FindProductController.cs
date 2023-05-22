
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
        ICheckSendEmail CheckSendEmail;
        IWriteFormatEmail WriteFormatEmail;
        public FindProductController(IEnumerable<ISearch> Searches, ICheckSendEmail Check, IWriteFormatEmail Write)
        {
            Search = Searches;
            CheckSendEmail = Check;
            WriteFormatEmail = Write;
        }


        [HttpGet]
        public string GetAdvertisements([FromQuery] List<string> Filter)
        {
            List<Advertisements> Advertisements = new List<Advertisements>();

            foreach (string F in Filter)
            {
                foreach (ISearch Search in Search)
                {
                    Advertisements.AddRange(Search.GetAdvertisement(F));
                }
            }

            Advertisements = CheckSendEmail.ReadAdvertisementDB(Advertisements);
            CheckSendEmail.SaveAdvertisementDB(Advertisements);

            if (Advertisements.Count == 0)
            {
                return "There are no new adds.";
            }

            string HTML = WriteFormatEmail.FormatHtml(Advertisements);

            SendEmail SendEmail = new SendEmail();
            SendEmail.SendEmailToClient(HTML);

            return "Ok";
        }




    }
}
