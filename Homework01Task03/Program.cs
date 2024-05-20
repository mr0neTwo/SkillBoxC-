using System;
using System.Collections.Generic;
using Utilites;

namespace Homework01Task03
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			HashSet<int> hashSet = new();

			bool exitRequested = false;

			while (!exitRequested)
			{
				int number = Utils.GetIntFromConsole("Enter 0 to exit or any other number to continue: ");

				if (number == 0)
				{
					exitRequested = true;
				}
				else
				{
					if (hashSet.Contains(number))
					{
						Console.WriteLine($"Number {number} already added");
					}
					
					hashSet.Add(number);
					Console.WriteLine($"Unique numbers: [{string.Join(", ", hashSet)}]");
				}
			}
		}
	}
}
