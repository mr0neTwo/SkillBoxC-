using System;
using Utilites;

namespace Homework06
{
	internal class Program
	{
		private static Repository repository;
		
		public static void Main(string[] args)
		{
			bool exitRequested = false;
			repository = new Repository();
			PrintMainInstructions();

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
						GetDataMenu();
						
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
					PrintMainInstructions();
				}
			}
		}

		private static void GetDataMenu()
		{
			PrintGetDataInstruction();

			bool exitRequested = false;

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
						PrintAllWorker();
						exitRequested = true;

						break;
					}
					case "2":
					{
						FindById();
						exitRequested = true;

						break;
					}
					case "3":
					{
						FindByBirthDate();
						exitRequested = true;

						break;
					}
					default:
					{
						Console.WriteLine("Incorrect command");
						PrintGetDataInstruction();
						
						break;
					}
				}
			}
		}

		private static Worker[] SortMenu(Worker[] workers)
		{
			PrintSortMenuInstruction();
			
			while (true)
			{
				string command = Console.ReadLine();

				switch (command)
				{
					case "0":
					{
						return workers;
					}
					case "1":
					{
						Array.Sort(workers, (w1, w2) => w1.CreatedAt.CompareTo(w2.CreatedAt));

						return workers;
					}
					case "2":
					{
						Array.Sort(workers, (w1, w2) => String.Compare(w1.FullName, w2.FullName, StringComparison.Ordinal));

						return workers;
					}
					case "3":
					{
						Array.Sort(workers, (w1, w2) => w1.BirthDate.CompareTo(w2.BirthDate));

						return workers;
					}
					default:
					{
						Console.WriteLine("Incorrect command");
						PrintSortMenuInstruction();

						break;
					}
				}
			}
		}

		private static void CreateNewWorker()
		{
			Worker worker = GetWorkerDataFromConsole();
			repository.AddWorker(worker);
		}

		private static void PrintAllWorker()
		{
			Worker[] workers = repository.GetAllWorkers();
			Worker[] sortedWorkers = SortMenu(workers);
			PrintWorkers(sortedWorkers);
		}

		private static void FindById()
		{
			int id = Utils.GetIntFromConsole("Enter worker id: ");
			Worker worker = repository.GetWorkerById(id);

			if (string.IsNullOrEmpty(worker.FullName))
			{
				Console.WriteLine("Worker not found");
				
				return;
			}
			
			PrintWorker(worker);
		}

		private static void FindByBirthDate()
		{
			DateTime startDate = Utils.GetDateFromConsole("Enter start date: ");
			DateTime endDate = Utils.GetDateFromConsole("Enter end date: ");

			Worker[] workers = repository.GetWorkersBetweenTwoDates(startDate, endDate);
			Worker[] sortedWorkers = SortMenu(workers);
			PrintWorkers(sortedWorkers);
		}

		private static void PrintWorkers(Worker[] workers)
		{
			foreach (Worker worker in workers)
			{
				PrintWorker(worker);
			}
		}

		private static void PrintWorker(Worker worker)
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


		private static void PrintMainInstructions()
		{
			Console.WriteLine("Enter command:\n0 - exit\n1 - enter new employee data\n2 - print data");
		}

		private static void PrintGetDataInstruction()
		{
			Console.WriteLine("0 - previous menu\n1 - show all\n2 - find by Id\n3 - find by birth date");
		}

		private static void PrintSortMenuInstruction()
		{
			Console.WriteLine("0 - without sorting\n1 - sort by creating date\n2 - sort by name\n3 - sort by birth date");
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
