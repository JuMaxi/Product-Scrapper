
using Microsoft.AspNetCore.Mvc;
using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using ProductScrapper.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ProductScrapper.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class FindProductController : ControllerBase
    {
        IEnumerable<ISearch> Search;
        IAccessDataBase AccessDB;
        ICheckSendEmail CheckSendEmail;
        public FindProductController(IEnumerable<ISearch> Searches, IAccessDataBase Access, ICheckSendEmail Check) 
        {
            Search = Searches;
            AccessDB = Access;
            CheckSendEmail = Check;
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

            WriteFormatEmail Write = new WriteFormatEmail();
            string HTML = Write.FormatHtml(Advertisements);

            SendEmail SendEmail = new SendEmail();
            SendEmail.SendEmailToClient(HTML);

            return "Ok";
        }



       
    }
}
