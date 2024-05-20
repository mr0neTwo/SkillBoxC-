using System;
using System.Collections.Generic;

namespace Homework08Task01
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			List<int> numbers = GetRandomIntegerList(100, 0, 100);
			PrintIntegerList(numbers);
			RemoveAllValueInRange(numbers, 25, 50);
			PrintIntegerList(numbers);
		}

		private static List<int> GetRandomIntegerList(int count, int startValue, int endValue)
		{
			Random random = new Random();
			List<int> numbers = new List<int>();

			for (int i = 0; i < count; i++)
			{
				numbers.Add(random.Next(startValue, endValue));
			}

			return numbers;
		}

		private static void PrintIntegerList(List<int> list)
		{
			Console.WriteLine(string.Join(" ", list));
		}

		private static void RemoveAllValueInRange(List<int> list, int startRange, int endRange)
		{
			list.RemoveAll(number => number > startRange && number < endRange);
		}
	}
}
