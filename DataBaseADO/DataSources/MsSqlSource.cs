using System;
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
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				throw;
			}
		}
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
				command.Parameters.AddWithValue("@LastName", client.SecondName);
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
			command.Parameters.AddWithValue("@FirstName", client.FirstName);
			command.Parameters.AddWithValue("@LastName", client.SecondName);
			command.Parameters.AddWithValue("@ThirdName", client.ThirdName);
			command.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
			command.Parameters.AddWithValue("@Email", client.Email);

			command.ExecuteNonQuery();
		}
	}
}
