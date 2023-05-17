using HtmlAgilityPack;
using Microsoft.Data.SqlClient;
using ProductScrapper.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ProductScrapper.Services
{
    public class AccessDataBase : IAccessDataBase
    {

        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=ProductScrapper;User Id=sa;Password=S4root;TrustServerCertificate=True";

        public void AccessNonQuery(string Action)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            {
                SqlCommand Command = new SqlCommand(Action, Connection);
                Connection.Open();
                Command.ExecuteNonQuery();
            }
        }

        public IDataReader AccessReader(string Action)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            SqlCommand Command = new SqlCommand(Action, Connection);
            Connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();

            return Reader;
        }

    }
}
