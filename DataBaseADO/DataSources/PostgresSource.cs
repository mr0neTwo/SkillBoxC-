using System;
using System.Collections.Generic;
using System.Data;
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
				transaction.Commit();
				Console.WriteLine($"Order {order.ProductName} added");
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

	public Order[] GetOrders(string email)
	{
		List<Order> orders = new();
		DataTable orderTable = new();
		
		using (NpgsqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (NpgsqlCommand command = new(Query.GetOrdersByEmail, connection))
			{
				command.Parameters.AddWithValue("@Email", email);
				
				
				using (NpgsqlDataAdapter dataAdapter = new(command))
				{
					dataAdapter.Fill(orderTable);
				}
			}
		}

		foreach (DataRow dataRow in orderTable.Rows)
		{
			Order order = new()
			{
				Id = (int)dataRow["Id"],
				Email = dataRow["Email"].ToString(),
				ProductCode = (int)dataRow["ProductCode"],
				ProductName = dataRow["ProductName"].ToString()
			};
			
			orders.Add(order);
		}

		return orders.ToArray();
	}

	private static void AddOrder(NpgsqlConnection connection, NpgsqlTransaction transaction, Order order)
	{
		using (NpgsqlCommand command = new(Query.InsertOrder, connection, transaction))
		{
			command.Parameters.AddWithValue("@ProductCode", order.ProductCode);
			command.Parameters.AddWithValue("@ProductName", order.ProductName ?? string.Empty);
			command.Parameters.AddWithValue("@Email", order.Email);

			command.ExecuteNonQuery();
		}
	}

	public void RemoveOrder(Order order)
	{
		using (NpgsqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (NpgsqlCommand command = new(Query.DeleteOrder, connection))
			{
				command.Parameters.AddWithValue("@Id", order.Id);
				command.ExecuteNonQuery();
			}
		}
	}

	public void RemoveAllOrders(string email)
	{
		using (NpgsqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (NpgsqlCommand command = new(Query.DeleteAllOrder, connection))
			{
				command.Parameters.AddWithValue("@Email", email);
				command.ExecuteNonQuery();
			}
		}
	}
}
