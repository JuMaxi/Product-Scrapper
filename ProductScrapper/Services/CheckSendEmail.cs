using ProductScrapper.Interfaces;
using ProductScrapper.Models;
using System.Collections.Generic;
using System.Data;

namespace ProductScrapper.Services
{
    public class CheckSendEmail : ICheckSendEmail
    {
        IAccessDataBase AccessDB;

        public CheckSendEmail(IAccessDataBase accessDB)
        {
            AccessDB = accessDB;
        }

        public void SaveAdvertisementDB(List<Advertisements> Advertisements)
        {
            foreach (Advertisements Ad in Advertisements)
            {
                string Insert = "insert into Advertisements (Url, Product, ImageProduct) values ('" + Ad.Url + "','" + Ad.Product + "','" + Ad.ImageProduct + "')";

                AccessDB.AccessNonQuery(Insert);
            }
        }

        public List<Advertisements> ReadAdvertisementDB(List<Advertisements> Advertisements)
        {
            for (int Position = 0; Position < Advertisements.Count; Position++)
            {
                string Select = "select * from Advertisements where Product='" + Advertisements[Position].Product + "'";

                IDataReader Reader = AccessDB.AccessReader(Select);

                while (Reader.Read())
                {
                    Advertisements.Remove(Advertisements[Position]);

                    Position = Position - 1;

                    break;
                    
                }
            }
            return Advertisements;
        }

        

    }
}
