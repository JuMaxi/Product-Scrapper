﻿using System.Net.Mail;
using System.Net;
using ProductScrapper.Models;
using ProductScrapper.Interfaces;
using System.Collections.Generic;

namespace ProductScrapper.Services
{
    public class SendEmail : ISendEmail
    {
        private string ReadFile()
        {
            string Path = @"C:\Dev\Password.txt";

            if (System.IO.File.Exists(Path))
            {
                string Text = System.IO.File.ReadAllText(Path);

                return Text;
            }
            return null;
        }
        public void SendEmailToClient(string HTML, string Email)
        {
            string Password = ReadFile();

            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential("jmaximovski@gmail.com", Password);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress("jmaximovski@gmail.com");

                    smtpClient.Host = "smtp-relay.sendinblue.com";
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;

                    message.From = fromAddress;
                    message.Subject = "New items found by Scrapper";
                    message.IsBodyHtml = true;
                    message.Body = HTML;
                    message.To.Add(Email);

                    smtpClient.Send(message);
                }
            }

        }
    }
}
