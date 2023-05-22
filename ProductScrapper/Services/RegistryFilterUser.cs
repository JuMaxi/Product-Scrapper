using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System;
using System.Data;
using System.Globalization;

namespace ProductScrapper.Services
{
    public class RegistryFilterUser : IRegistryFilterUser
    {
        IAccessDataBase AccessDB;

        public RegistryFilterUser(IAccessDataBase Accessdb) 
        { 
            AccessDB = Accessdb;
        }

        public void RegistryEmailUser(string Email)
        {
            string Insert = "insert into Users (Email) values ('" + Email + "')";
            AccessDB.AccessNonQuery(Insert);
        }

        public void RegistryFilterToUser(int Id, string Filter)
        {
            
            string Insert = "insert into RelationalUsersFilter (UserId, Filter) values (" +Id + ",'" + Filter + "')";
            AccessDB.AccessNonQuery(Insert);
        }

        

    }
}
