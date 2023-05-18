
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
        public string GetAdvertisements([FromQuery] string Filter)
        {
            List<Advertisements> Advertisements = new List<Advertisements>();
            foreach(ISearch Search in Search)
            {
                Advertisements.AddRange(Search.GetAdvertisement(Filter));
            }

            Advertisements = CheckSendEmail.CheckIfAdIsNew(Advertisements);
            CheckSendEmail.SaveAdvertisementDB(Advertisements);

            string HTML = WriteFormatEmail.FormatHtml(Advertisements);

            SendEmail SendEmail = new SendEmail();
            SendEmail.SendEmailToClient(HTML);

            return "Ok";
        }



       
    }
}
