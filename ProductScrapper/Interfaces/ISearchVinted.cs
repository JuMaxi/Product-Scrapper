﻿using ProductScrapper.Models;
using System.Collections.Generic;

namespace ProductScrapper.Interfaces
{
    public interface ISearchVinted
    {
        public List<Advertisements> ReturnHRefAndProduct(string Filter);
    }
}
