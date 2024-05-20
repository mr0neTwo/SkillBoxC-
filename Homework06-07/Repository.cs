using System;
using System.Collections.Generic;
using System.IO;

namespace Homework06
{
	public sealed class Repository
	{
		public const char Separator = '#';
		private static readonly string EmployeeDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.txt");
		private static readonly string IdDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "id.txt");
		
		
		private int _idCounter;

		public Repository()
		{
			LoadCurrentId();
		}


		public Worker[] GetAllWorkers()
		{
			if (!File.Exists(EmployeeDataPath))
			{
				return new Worker[] { };
			}
			
			string[] lines = File.ReadAllLines(EmployeeDataPath);
			Worker[] workers = new Worker[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				workers[i] = new Worker(lines[i]);
			}
			
			return workers;
		}

		public Worker GetWorkerById(int id)
		{
			Worker[] workers = GetAllWorkers();
			
			foreach (Worker worker in workers)
			{
				if (worker.Id == id)
				{
					return worker;
				}
			}

			return new Worker();
		}

		public void DeleteWorker(int id)
		{
			Worker[] workers = GetAllWorkers();
			File.WriteAllText(EmployeeDataPath, string.Empty);
			
			using (StreamWriter writer = new StreamWriter(EmployeeDataPath, append : true))
			{
				foreach (Worker worker in workers)
				{
					if (worker.Id != id)
					{
						writer.WriteLine(worker.ToString());
					}
				}
			}
		}

		public void AddWorker(Worker worker)
		{
			IncreaseIdCounter();
			
			worker.Id = _idCounter;
			worker.CreatedAt = DateTime.Now;

			WriteNewWorker(worker);
		}

		public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
		{
			Worker[] workers = GetAllWorkers();

			List<Worker> searchResult = new List<Worker>();

			foreach (Worker worker in workers)
			{
				if (worker.BirthDate >= dateFrom && worker.BirthDate <= dateTo)
				{
					searchResult.Add(worker);
				}
			}

			return searchResult.ToArray();
		}

		private void WriteNewWorker(Worker worker)
		{
			using (StreamWriter writer = new StreamWriter(EmployeeDataPath, append : true))
			{
				writer.WriteLine(worker.ToString());
			}
		}

		private void IncreaseIdCounter()
		{
			_idCounter++;
			File.WriteAllText(IdDataPath, _idCounter.ToString());
		}

		private void LoadCurrentId()
		{
			if (!File.Exists(IdDataPath))
			{
				return;
			}

			string idString = File.ReadAllText(IdDataPath);

			if (int.TryParse(idString, out int id))
			{
				_idCounter = id;
			}
		}
	}
}
