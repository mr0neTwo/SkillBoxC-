using DataBaseADO;
using System.Data;

namespace ElectronicShop.Models
{
    public sealed class DataProvider
    {
        private readonly Database _database = new();

        public DataTable GetAllClients()
        {
            return _database.GetClientsTable();
        }

        public DataTable GetOrders()
        {
            return _database.GetOrdersTable();
        }
        
        public DataTable GetData()
        {
            DataTable table = new ();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Email", typeof(string));

            // Заполнение примерами данных
            table.Rows.Add(1, "Alice", "alice@example.com");
            table.Rows.Add(2, "Bob", "bob@example.com");
            table.Rows.Add(3, "Charlie", "charlie@example.com");

            return table;
        }
    }
}
