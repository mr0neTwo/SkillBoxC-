using System;

namespace Task01
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			int rowCount = GetIntFromConsole("Enter number of rows: ");
			int columnCount = GetIntFromConsole("Enter number of column: ");

			int[,] randomMatrix = GenerateRandomValueMatrix(rowCount, columnCount, 0, 100);
			PrintMatrix(randomMatrix);

			int matrixSum = CalculateMatrixSum(randomMatrix);
			Console.WriteLine($"Sum: {matrixSum}");
		}

		private static int GetIntFromConsole(string message)
		{
			while (true)
			{
				Console.Write(message);
				string input = Console.ReadLine();
				
				if (int.TryParse(input, out int number))
				{
					return number;
				}
				
				Console.WriteLine("Entered value is not integer");
			}
		}

		private static int[,] GenerateRandomValueMatrix(int rowCount, int columnCount, int startValueRange, int endValueRange)
		{
			int[,] matrix = new int[rowCount, columnCount];
			Random random = new Random();

			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					matrix[i, j] = random.Next(startValueRange, endValueRange + 1);
				}
			}

			return matrix;
		}

		private static void PrintMatrix(int[,] matrix)
		{
			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int column = 0; column < matrix.GetLength(1); column++)
				{
					Console.Write($"{matrix[row, column],4}");
				}
				
				Console.WriteLine("");
			}
		}

		private static int CalculateMatrixSum(int[,] matrix)
		{
			int sum = 0;
			
			for (int row = 0; row < matrix.GetLength(0); row++)
			{
				for (int column = 0; column < matrix.GetLength(1); column++)
				{
					sum += matrix[row, column];
				}
			}

			return sum;
		}
	}
}
