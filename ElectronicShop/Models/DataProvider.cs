using DataBaseADO;

namespace ElectronicShop.Models
{
    public sealed class DataProvider
    {
        private readonly Database _database;

        public DataProvider()
        {
            _database = new Database();
            _database.RestoreDefaultTables();
        }

        public Client[] GetAllClients()
        {
            return _database.GetAllClients();
        }

        public Order[] GetOrders(string email)
        {
            return _database.GetOrders(email);
        }

        public void AddClient(Client client)
        {
            _database.AddClient(client);
        }

        public void UpdateClient(Client client)
        {
            _database.UpdateClient(client);
        }

        public void RemoveClient(Client client)
        {
            _database.RemoveClient(client);
        }

        public void RemoveAllClients()
        {
            _database.RemoveAllClients();
        }

        public void AddOrder(Order order)
        {
            _database.AddOrder(order);
        }

        public void RemoveOrder(Order order)
        {
            _database.RemoveOrder(order);
        }

        public void RemoveAllOrder(string email)
        {
            _database.RemoveAllOrders(email);
        }
    }
}
