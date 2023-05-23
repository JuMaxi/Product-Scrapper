using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISendEmail
    {
        public void SendEmailToClient(string HTML, string Email);

    }
}
