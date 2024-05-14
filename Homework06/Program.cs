using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Homework06
{
	internal class Program
	{
		private const char Separator = '#';
		private static readonly string EmployeeDataPath = "../../data.txt";
		private static readonly string IdDataPath = "../../id.txt";
		private static int idCounter;
		private static string[] columnNames = { "Full name", "Age", "Height", "Birth date", "Birthplace" };
		
		public static void Main(string[] args)
		{
			bool exitRequested = false;
			idCounter = LoadCurrentId();
			PrintInstructions();

			while (!exitRequested)
			{
				string command = Console.ReadLine();

				switch (command)
				{
					case "0":
					{
						exitRequested = true;
						
						break;
					}
					case "1":
					{
						CreateNewEmployee();
						
						break;
					}
					case "2":
					{
						PrintData();
						
						break;
					}
					default:
					{
						Console.WriteLine("Incorrect command");

						break;
					}
				}

				if (!exitRequested)
				{
					PrintInstructions();
				}
			}
		}

		private static int LoadCurrentId()
		{
			if (File.Exists(IdDataPath))
			{
				string idString = File.ReadAllText(IdDataPath);

				if (int.TryParse(idString, out int id))
				{
					return id;
				}
			}

			return 0;
		}

		private static void PrintData()
		{
			if (!File.Exists(EmployeeDataPath))
			{
				Console.WriteLine("No data");
				
				return;
			}
			
			string[] lines = File.ReadAllLines(EmployeeDataPath);

			foreach (string line in lines)
			{
				string[] values = line.Split(Separator); 
				Console.WriteLine($"Id: {values[0]}");
				Console.WriteLine($"Created at: {values[1]}");

				for (int i = 0; i < columnNames.Length; i++)
				{
					Console.WriteLine($"{columnNames[i]}: {values[i + 2]}");
				}
				
				Console.WriteLine();
			}
		}

		private static void CreateNewEmployee()
		{
			string employData = GetEmployeeDataFromConsole();
			
			using (StreamWriter writer = new StreamWriter(EmployeeDataPath, append : true))
			{
				writer.WriteLine(employData);
			}

			IncreaseIdCounter();
		}

		private static void IncreaseIdCounter()
		{
			idCounter++;
			File.WriteAllText(IdDataPath, idCounter.ToString());
		}

		private static void PrintInstructions()
		{
			Console.WriteLine("Enter command:\n0 - exit\n1 - enter new employee data\n2 - print data");
		}

		private static string GetEmployeeDataFromConsole()
		{
			List<string> dataList = new List<string>();

			int id = idCounter;
			dataList.Add(id.ToString());
			
			DateTime date = DateTime.Now;
			dataList.Add(date.ToString(CultureInfo.InvariantCulture));

			Console.WriteLine("Enter employee data:");

			foreach (string columnName in columnNames)
			{
				Console.Write($"{columnName}: ");
				string input = Console.ReadLine();
				dataList.Add(input);
			}
			
			return string.Join(Separator.ToString(), dataList);
		}
	}
}
