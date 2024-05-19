using System;
using Utilites;

namespace Homework06
{
	internal class Program
	{
		private static string[] columnNames = { "Full name", "Age", "Height", "Birth date", "Birthplace" };
		private static Repository repository;
		
		public static void Main(string[] args)
		{
			bool exitRequested = false;
			repository = new Repository();
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
						CreateNewWorker();
						
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

		private static void PrintData()
		{
			Worker[] workers = repository.GetAllWorkers();

			foreach (Worker worker in workers)
			{
				Console.WriteLine($"Id: {worker.Id}");
				Console.WriteLine($"Created at: {worker.CreatedAt:dd.MM.yyyy}");
				Console.WriteLine($"FullName: {worker.FullName}");
				Console.WriteLine($"Age: {worker.Age}");
				Console.WriteLine($"Height: {worker.Height}");
				Console.WriteLine($"Birth date: {worker.BirthDate:dd.MM.yyyy}");
				Console.WriteLine($"Birth place: {worker.BirthPlace}");
				Console.WriteLine();
			}
		}

		private static void CreateNewWorker()
		{
			Worker worker = GetWorkerDataFromConsole();
			repository.AddWorker(worker);
		}

		
		private static void PrintInstructions()
		{
			Console.WriteLine("Enter command:\n0 - exit\n1 - enter new employee data\n2 - print data");
		}

		private static Worker GetWorkerDataFromConsole()
		{
			Worker worker = new Worker();
			
			Console.WriteLine("Enter employee data:");

			worker.FullName = Utils.GetStringFromConsole("Enter full name: ");
			worker.Age = Utils.GetIntFromConsole("Enter age: ");
			worker.Height = Utils.GetIntFromConsole("Enter height: ");
			worker.BirthDate = Utils.GetDateFromConsole("Enter birth date: ");
			worker.BirthPlace = Utils.GetStringFromConsole("Enter birth place: ");

			return worker;
		}
	}
}
