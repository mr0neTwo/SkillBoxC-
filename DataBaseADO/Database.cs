using System.Data;
using DataBaseADO.DataSources;

namespace DataBaseADO
{
	public sealed class Database
	{
		private readonly MsSqlSource _msSqlSource;
		private readonly PostgresSource _postgresSource;

		public Database()
		{
			_msSqlSource = new MsSqlSource();
			_postgresSource = new PostgresSource();
		}

		public void TestConnect()
		{
			_postgresSource.TestConnection();
			_msSqlSource.TestConnection();
		}

		public void RestoreDefaultTables()
		{
			_msSqlSource.RestoreDefaultValues();
			_postgresSource.RestoreDefaultValues();
		}

		public DataTable GetClientsTable()
		{
			return _msSqlSource.GetDataTable(Query.GetAllClients);
		}

		public Client[] GetAllClients()
		{
			return _msSqlSource.GetAllClients();
		}

		public void AddClient(Client client)
		{
			_msSqlSource.AddClient(client);
		}

		public Order[] GetOrders(string email)
		{
			return _postgresSource.GetOrders(email);
		}

		public DataTable GetOrdersTable()
		{
			return _postgresSource.GetDataTable(Query.GetAllOrders);
		}

		public void UpdateClient(Client client)
		{
			_msSqlSource.UpdateClient(client);
		}

		public void RemoveClient(Client client)
		{
			_msSqlSource.RemoveClient(client);
		}

		public void RemoveAllClients()
		{
			_msSqlSource.RemoveAllClients();
		}

		public void AddOrder(Order order)
		{
			_postgresSource.AddOrder(order);
		}

		public void RemoveOrder(Order order)
		{
			_postgresSource.RemoveOrder(order);
		}

		public void RemoveAllOrders(string email)
		{
			_postgresSource.RemoveAllOrders(email);
		}
	}
}
