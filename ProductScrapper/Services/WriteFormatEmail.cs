using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Services
{
    public class WriteFormatEmail : IWriteFormatEmail
    {
        public string FormatHtml(List<Advertisements> Advertisement)
        {
            string HTML = @"
             <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Product Scrapper</title>
                    <style>
                        body{
                            background-color: #ebe1c629;
                            font-family: Arial, Helvetica, sans-serif;
                        }
                        h1{
                            color:#42a742;
                            text-align: center;
                            text-shadow: 0px 1px 1px rgba(0, 0, 0, 0.248);
                        }
                        img{
                            width: 200px;
                            height: 200px;
                            margin: 5px;
                            border-radius: 4px;
                            box-shadow: 0px 5px 5px rgba(0, 0, 0, 0.377);
                            padding: 3px;
                        }
                        #product-main{
                            float: left;
                            color: #4343e5d7;
                            font-size: 14px;
                            text-shadow: 0px 1px 1px rgba(0, 0, 0, 0.292);
                            padding: 3px;
                        }
                        #product-title{
                            text-align: center;
                            width: 200px;
                            height: 40px;
                        }
                    </style>
                </head>
                <body>
                    <h1>New Advertisements</h1>
                ";
            foreach (Advertisements Ad in Advertisement)
            {
                HTML = HTML + "<div id=\"product-main\"> " +
                                    "<div id=\"product-title\">" + Ad.Product + "</div>" +
                                        "<div id=\"product-image\">" +
                                            "<a href=" + Ad.Url + ">" +
                                                "<img src=" + Ad.ImageProduct + ">" +
                                            "</a>" +
                                        "</div>" +
                              "</div>";
            }
            HTML = HTML + "</body>" + "</html>";
            return HTML;
        }
    }
}
