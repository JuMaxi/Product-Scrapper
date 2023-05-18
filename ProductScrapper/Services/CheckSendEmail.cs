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
            AccessDB= accessDB;
        }

        public void SaveAdvertisementDB(List<Advertisements> Advertisements)
        {
            foreach (Advertisements Ad in Advertisements)
            {
                string Insert = "insert into Advertisements (Url, Product, ImageProduct) values ('" + Ad.Url + "','" + Ad.Product + "','" + Ad.ImageProduct + "')";

                AccessDB.AccessNonQuery(Insert);
            }
        }

        private List<Advertisements> ReadAdvertisementDB()
        {
            string Select = "select * from Advertisements";
            List<Advertisements> NewAdvertisements = new List<Advertisements>();

            IDataReader Reader = AccessDB.AccessReader(Select);

            while (Reader.Read())
            {
                Advertisements Ad = new Advertisements();

                Ad.Url = Reader["Url"].ToString();
                Ad.Product = Reader["Product"].ToString();
                Ad.ImageProduct = Reader["ImageProduct"].ToString();

                NewAdvertisements.Add(Ad);
            }
            return NewAdvertisements;
        }

        public List<Advertisements> CheckIfAdIsNew(List<Advertisements> Advertisements)
        {
            List<Advertisements> SavedAdvertisements = ReadAdvertisementDB();

            for (int Position = 0; Position < SavedAdvertisements.Count; Position++)
            {
                for (int PositionAd = 0; PositionAd < Advertisements.Count; PositionAd++)
                {
                    if (Advertisements[PositionAd].Product == SavedAdvertisements[Position].Product)
                    {
                        Advertisements.Remove(Advertisements[PositionAd]);

                        break;
                    }
                }
            }
            return Advertisements;
        }

       
    }
}
