using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataBaseADO.DataSources;

internal sealed class MsSqlSource : DataSource<SqlConnection, SqlCommand, SqlDataAdapter>
{
	public void AddClient(Client client)
	{
		using (SqlConnection connection = new(ConnectionString))
		{
			try
			{
				connection.Open();
				SqlTransaction transaction = connection.BeginTransaction();
				AddClient(connection, transaction, client);
				transaction.Commit();
				Console.WriteLine($"Client {client.FirstName} added");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				throw;
			}
		}
	}

	public Client[] GetAllClients()
	{
		DataTable clientTable = GetDataTable(Query.GetAllClients);
		List<Client> clients = new();

		foreach (DataRow row in clientTable.Rows)
		{
			Client client = new()
			{
				Id = (int)row["Id"],
				FirstName = row["FirstName"].ToString(),
				LastName = row["LastName"].ToString(),
				ThirdName = row["ThirdName"].ToString(),
				PhoneNumber = row["PhoneNumber"].ToString(),
				Email = row["Email"].ToString(),
			};
			
			clients.Add(client);
		}

		return clients.ToArray();
	}

	public void UpdateClient(Client client)
	{
		using (SqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (SqlCommand command = new(Query.UpdateClient, connection))
			{
				command.Parameters.AddWithValue("@Id", client.Id);
				command.Parameters.AddWithValue("@FirstName", client.FirstName);
				command.Parameters.AddWithValue("@LastName", client.LastName);
				command.Parameters.AddWithValue("@ThirdName", client.ThirdName);
				command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
				command.Parameters.AddWithValue("@Email", client.Email);
				
				command.ExecuteNonQuery();
			}
		}
	}

	public override void RestoreDefaultValues()
	{
		DropAllTables(Query.GetAllMsSqlTablesQuery);
		CreateDefaultClientTable();
	}

	protected override string GetConnectionString()
	{
		SqlConnectionStringBuilder aqlConnectionStringBuilder = new()
		{
			DataSource = @"(localdb)\MSSQLLocalDB", InitialCatalog = "skillbox_test", Pooling = true
		};

		return aqlConnectionStringBuilder.ConnectionString;
	}

	private void CreateDefaultClientTable()
	{
		using (SqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			SqlTransaction transaction = connection.BeginTransaction();

			try
			{
				using (SqlCommand command = new(Query.CreateClientTable, connection, transaction))
				{
					command.ExecuteNonQuery();
				}

				foreach (Client client in DefaultValues.Сlients)
				{
					AddClient(connection, transaction, client);
				}

				transaction.Commit();
				Console.WriteLine("Table clients created");
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				Console.WriteLine("Transaction rolled back");

				throw;
			}
		}
	}

	private static void AddClient(SqlConnection connection, SqlTransaction transaction, Client client)
	{
		using (SqlCommand command = new(Query.InsertClient, connection, transaction))
		{
			command.Parameters.AddWithValue("@FirstName", client.FirstName ?? string.Empty);
			command.Parameters.AddWithValue("@LastName", client.LastName ?? string.Empty);
			command.Parameters.AddWithValue("@ThirdName", client.ThirdName ?? string.Empty);
			command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber ?? string.Empty);
			command.Parameters.AddWithValue("@Email", client.Email);

			command.ExecuteNonQuery();
		}
	}

	public void RemoveClient(Client client)
	{
		using (SqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (SqlCommand command = new(Query.DeleteClient, connection))
			{
				command.Parameters.AddWithValue("@Id", client.Id);
				
				command.ExecuteNonQuery();
			}
		}
	}

	public void RemoveAllClients()
	{
		using (SqlConnection connection = new(ConnectionString))
		{
			connection.Open();
			
			using (SqlCommand command = new(Query.DeleteAllClient, connection))
			{
				command.ExecuteNonQuery();
			}
		}
	}
}
