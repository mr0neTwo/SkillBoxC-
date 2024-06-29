using System;
using Npgsql;

namespace DataBaseADO.DataSources;

internal sealed class PostgresSource : DataSource<NpgsqlConnection, NpgsqlCommand, NpgsqlDataAdapter>
{
	public void AddOrder(Order order)
	{
		using (NpgsqlConnection connection = new(ConnectionString))
		{
			try
			{
				connection.Open();
				NpgsqlTransaction transaction = connection.BeginTransaction();
				AddOrder(connection, transaction, order);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				throw;
			}
		}
	}
	
	public override void RestoreDefaultValues()
	{
		DropAllTables(Query.GetAllPostgresTablesQuery);
		CreateDefaultOrderTable();
	}

	protected override string GetConnectionString()
	{
		NpgsqlConnectionStringBuilder postgresConnectionStringBuilder = new()
		{
			Host = "localhost",
			Port = 5432,
			Database = "skillbox_test",
			Username = "postgres",
			Password = "225567",
			Pooling = true
		};

		return postgresConnectionStringBuilder.ConnectionString;
	}

	private void CreateDefaultOrderTable()
	{
		using (NpgsqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			NpgsqlTransaction transaction = connection.BeginTransaction();

			try
			{
				using (NpgsqlCommand command = new(Query.CreateOrderTable, connection, transaction))
				{
					command.ExecuteNonQuery();
				}

				foreach (Order order in DefaultValues.Orders)
				{
					AddOrder(connection, transaction, order);
				}

				transaction.Commit();
				Console.WriteLine("Table orders created");
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				Console.WriteLine("Transaction rolled back");
					
				throw;
			}
		}
	}
	
	private static void AddOrder(NpgsqlConnection connection, NpgsqlTransaction transaction, Order order)
	{
		using (NpgsqlCommand command = new(Query.InsertOrder, connection, transaction))
		{
			command.Parameters.AddWithValue("@ProductCode", order.ProductCode);
			command.Parameters.AddWithValue("@ProductName", order.ProductName);
			command.Parameters.AddWithValue("@Email", order.Email);

			command.ExecuteNonQuery();
		}
	}
}
