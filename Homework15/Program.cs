using System;
using System.Threading;
using System.Threading.Tasks;

namespace Homework15
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine($"Main thread id: {Thread.CurrentThread.ManagedThreadId}");
			
			Task task1 = PrintAsync("message one", 5f);
			Task task2 = PrintAsync("message two", 10f);
			Task task3 = PrintAsync("message three", 15f);

			await Task.WhenAll(task1, task2, task3);
		}

		private static async Task PrintAsync(string message, float delaySec)
		{
			int delayMc = (int)(delaySec * 1000);
			await Task.Delay(delayMc);
			Console.WriteLine($"Message: {message} Thread id: {Thread.CurrentThread.ManagedThreadId}");
		}
	}
}
