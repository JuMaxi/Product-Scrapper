using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface IWriteFormatEmail
    {
        public string FormatHtml(List<Advertisements> Advertisement);
    }
}
