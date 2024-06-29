using System;
using System.Data;
using System.Text;
using DataBaseADO;

namespace TestADO
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Database database = new();
			database.TestConnect();
			database.RestoreDefaultTables();

			DataTable clientTable = database.GetClientsTable();
			DataTable ordersTable = database.GetOrdersTable();

			PrintClients(clientTable);
			PrintOrders(ordersTable);

			DataSet dataSet = new();
			dataSet.Tables.Add(clientTable);
			dataSet.Tables.Add(ordersTable);
		}

		private static void PrintOrders(DataTable ordersTable)
		{
			StringBuilder stringBuilder = new();

			stringBuilder.Append("Orders:");
			stringBuilder.Append("\n");
			
			stringBuilder.Append($"{ordersTable.Columns[0].ColumnName,-4}");
			stringBuilder.Append($"{ordersTable.Columns[1].ColumnName,-15}");
			stringBuilder.Append($"{ordersTable.Columns[2].ColumnName,-22}");
			stringBuilder.Append($"{ordersTable.Columns[3].ColumnName,-20}");
			stringBuilder.Append("\n");
			
			foreach (DataRow dataRow in ordersTable.Rows)
			{
				stringBuilder.Append($"{(int)dataRow["Id"],-4}");
				stringBuilder.Append($"{(int)dataRow["ProductCode"],-15}");
				stringBuilder.Append($"{(string)dataRow["ProductName"],-22}");
				stringBuilder.Append($"{(string)dataRow["Email"],-20}");
				stringBuilder.Append("\n");
			}

			Console.WriteLine(stringBuilder.ToString());
		}

		private static void PrintClients(DataTable clientTable)
		{
			StringBuilder stringBuilder = new();

			stringBuilder.Append("Clients:");
			stringBuilder.Append("\n");
			
			stringBuilder.Append($"{clientTable.Columns[0].ColumnName,-4}");
			stringBuilder.Append($"{clientTable.Columns[1].ColumnName,-10}");
			stringBuilder.Append($"{clientTable.Columns[2].ColumnName,-10}");
			stringBuilder.Append($"{clientTable.Columns[3].ColumnName,-10}");
			stringBuilder.Append($"{clientTable.Columns[4].ColumnName,-20}");
			stringBuilder.Append($"{clientTable.Columns[5].ColumnName,-20}");
			stringBuilder.Append("\n");
			
			foreach (DataRow dataRow in clientTable.Rows)
			{
				stringBuilder.Append($"{(int)dataRow["Id"],-4}");
				stringBuilder.Append($"{(string)dataRow["FirstName"],-10}");
				stringBuilder.Append($"{(string)dataRow["LastName"],-10}");
				stringBuilder.Append($"{(string)dataRow["ThirdName"],-10}");
				stringBuilder.Append($"{(string)dataRow["PhoneNumber"],-20}");
				stringBuilder.Append($"{(string)dataRow["Email"],-20}");
				stringBuilder.Append("\n");
			}

			Console.WriteLine(stringBuilder.ToString());
		}
	}
}
