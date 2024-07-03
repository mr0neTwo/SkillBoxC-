using DatabaseEF;
using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicShop.Models
{
    public sealed class DataProvider
    {
        private readonly DatabaseContext _database;

        public DataProvider()
        {
            _database = new DatabaseContext();
            _database.FillDefaultValues();
        }

        public Client[] GetAllClients()
        {
            return _database.Clients.Include(client => client.Orders).ToArray();
        }

        public void AddClient(Client client)
        {
            _database.Clients.Add(client);
            _database.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            _database.Clients.Update(client);
            _database.SaveChanges();
        }

        public void RemoveClient(Client client)
        {
            _database.Clients.Remove(client);
            _database.SaveChanges();
        }

        public void RemoveAllClients()
        {
            _database.Clients.RemoveRange(_database.Clients.ToArray());
            _database.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            _database.Orders.Add(order);
            _database.SaveChanges();
        }

        public void RemoveOrder(Order order)
        {
            _database.Orders.Remove(order);
            _database.SaveChanges();
        }

        public void RemoveAllOrder(int clientId)
        {
            Order[] orders = _database.Orders.Where(order => order.ClientId == clientId).ToArray();
            _database.Orders.RemoveRange(orders);
            _database.SaveChanges();
        }
    }
}
