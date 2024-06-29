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

		public DataTable GetOrdersTable()
		{
			return _postgresSource.GetDataTable(Query.GetAllOrders);
		}
	}
}
