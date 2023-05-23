
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
        ISendEmail SendEmail;
        ICheckSendEmail CheckSendEmail;
        IWriteFormatEmail WriteFormatEmail;
        IRegistryFilterUser RegistryFilterUser;
        public FindProductController(IEnumerable<ISearch> Searches,ISendEmail Send, ICheckSendEmail Check, IWriteFormatEmail Write, IRegistryFilterUser FilterUser)
        {
            Search = Searches;
            SendEmail = Send;
            CheckSendEmail = Check;
            WriteFormatEmail = Write;
            RegistryFilterUser = FilterUser;
        }

        [HttpPost]
        public void InsertUserAndFilters(FilterUser New)
        {
            RegistryFilterUser.RegistryEmailUser(New);

            RegistryFilterUser.RegistryFilterToUser(New);
        }


        [HttpGet]
        public string GetAdvertisements()
        {
            List<FilterUser> FilterUser = RegistryFilterUser.ReadFiltersDB();

            List<Advertisements> Advertisements = new List<Advertisements>();

            foreach (FilterUser Filter in FilterUser)
            {
                foreach (ISearch Search in Search)
                {
                    foreach (string F in Filter.Filters)
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

                SendEmail.SendEmailToClient(HTML, Filter.Email);
            }
            return "Ok";
        }




    }
}
