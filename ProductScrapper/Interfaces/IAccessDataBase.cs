using System.Data;

namespace ProductScrapper.Interfaces
{
    public interface IAccessDataBase
    {
        void AccessNonQuery(string Action);
        IDataReader AccessReader(string Action);
    }
}