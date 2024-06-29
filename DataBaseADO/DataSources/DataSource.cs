using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataBaseADO.DataSources;

internal abstract class DataSource<TConnection, TCommand, TDataAdapter> where TConnection : DbConnection, new()
																		where TCommand : DbCommand, new()
																		where TDataAdapter : DbDataAdapter, new()
{
	protected string ConnectionString { get; }

	internal DataSource()
	{
		ConnectionString = GetConnectionString();
	}

	public abstract void RestoreDefaultValues();

	protected abstract string GetConnectionString();

	public void TestConnection()
	{
		using (TConnection connection = new())
		{
			Console.WriteLine($"Connection string: {ConnectionString}");

			try
			{
				connection.ConnectionString = ConnectionString;
				connection.Open();
				Console.WriteLine("Connection successful");
			}
			catch (Exception e)
			{
				Console.WriteLine("Connection failed");
				Console.WriteLine(e);

				throw;
			}
		}
	}
	
	public DataTable GetDataTable(string query)
	{
		DataTable dataTable = new();
		
		using (TConnection connection = new())
		{
			connection.ConnectionString = ConnectionString;
			connection.Open();

			using (TCommand command = new())
			{
				command.Connection = connection;
				command.CommandText = query;
				
				using (TDataAdapter dataAdapter = new())
				{
					dataAdapter.SelectCommand = command;
					dataAdapter.Fill(dataTable);
				}
			}
		}

		return dataTable;
	}

	protected void DropAllTables(string getAllTablesQuery)
	{
		using (TConnection connection = new())
		{
			connection.ConnectionString = ConnectionString;
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				DataTable tables = new();

				using (TCommand command = new())
				{
					command.Connection = connection;
					command.Transaction = transaction;
					command.CommandText = getAllTablesQuery;

					using (TDataAdapter adapter = new())
					{
						adapter.SelectCommand = command;
						adapter.Fill(tables);
					}
				}

				List<string> dropTableCommands = new();

				foreach (DataRow row in tables.Rows)
				{
					string tableName = row["TABLE_NAME"].ToString();
					dropTableCommands.Add($"DROP TABLE {tableName}");
				}

				foreach (string dropTableQuery in dropTableCommands)
				{
					using (TCommand dropTableCommand = new())
					{
						dropTableCommand.Connection = connection;
						dropTableCommand.Transaction = transaction;
						dropTableCommand.CommandText = dropTableQuery;
						dropTableCommand.ExecuteNonQuery();
						Console.WriteLine($"Table {dropTableQuery.Split()[2]} deleted.");
					}
				}

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				Console.WriteLine("Transaction rolled back");

				throw;
			}
		}
	}
}
