using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System;
using System.Collections.Generic;
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

        public void RegistryEmailUser(FilterUser New)
        {
            if(ReturnId(New) == 0) 
            {
                string Insert = "insert into Users (Email) values ('" + New.Email + "')";
                AccessDB.AccessNonQuery(Insert);
            }
        }

       
        private int ReturnId(FilterUser New)
        {
            string Select = "select * from Users where Email='" + New.Email + "'";

            IDataReader Reader = AccessDB.AccessReader(Select);

            while(Reader.Read())
            {
                return Convert.ToInt32(Reader["Id"]);
            }
            Reader.Close();

            return 0;
        }
        public void RegistryFilterToUser(FilterUser New)
        {
            int Id = ReturnId(New);

            foreach(string Filter in New.Filters)
            {
                string Insert = "insert into RelationalUsersFilter (UserId, Filter) values (" + Id + ",'" + Filter + "')";
                AccessDB.AccessNonQuery(Insert);
            }
          
        }
        public List<FilterUser> ReadFiltersDB()
        {
            List<FilterUser> Filters = new List<FilterUser>();

            string Select = "select Users.Id, Users.Email, RelationalUsersFilter.UserId, RelationalUsersFilter.Filter from Users " +
                "Inner Join RelationalUsersFilter On Users.Id = RelationalUsersFilter.UserId";
            int Id = 0;

            IDataReader Reader = AccessDB.AccessReader(Select);
            while(Reader.Read())
            {
                FilterUser New = new FilterUser();

                if (Convert.ToInt32(Reader["Id"]) != Id)
                {
                    List<string> F = new List<string>();
                    New.Email = Reader["Email"].ToString();
                    F.Add(Reader["Filter"].ToString());
                    New.Filters = F;

                    Filters.Add(New);
                    Id = Convert.ToInt32(Reader["Id"]);
                }
                else
                {
                    Filters[Filters.Count - 1].Filters.Add(Reader["Filter"].ToString());
                }
                
            }
            return Filters;
        }

        

    }
}
