using Microsoft.Data.SqlClient;
using ProductScrapper.Interfaces;
using System.Data;

namespace ProductScrapper.Services
{
    public class AccessDataBase : IAccessDataBase
    {
        private readonly SqlConnection Connection;
        string ConnectionString = "Server=LAPTOP-P4GEIO8K\\SQLEXPRESS;Database=ProductScrapper;User Id=sa;Password=S4root;TrustServerCertificate=True";
        public AccessDataBase()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

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
            SqlCommand Command = new SqlCommand(Action, Connection);
            SqlDataReader Reader = Command.ExecuteReader();

            return Reader;
        }

    }
}
