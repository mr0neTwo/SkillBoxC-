using System;

namespace Homework04Task02
{
	internal class Program
	{
		private static readonly Random Random = new Random();
		
		public static void Main(string[] args)
		{
			int[,] matrixA = GenerateRandomValueMatrix(2, 8, 0, 100);
			int[,] matrixB = GenerateRandomValueMatrix(2, 8, 0, 100);
			int[,] sumMatrix = AddMatrix(matrixA, matrixB);
			
			Console.WriteLine("MatrixA:");
			PrintMatrix(matrixA);
			Console.WriteLine("MatrixB:");
			PrintMatrix(matrixB);
			Console.WriteLine("SumMatrix:");
			PrintMatrix(sumMatrix);
		}

		private static int[,] AddMatrix(int[,] matrixA, int[,] matrixB)
		{
			int rowCount = matrixA.GetLength(0);
			int columnCount = matrixA.GetLength(1);

			if (matrixA.GetLength(0) != rowCount || matrixB.GetLength(1) != columnCount)
			{
				Console.WriteLine("These matrices cannot be added");

				return new int[0, 0];
			}

			int[,] sumMatrix = new int[rowCount, columnCount];
			
			for (int row = 0; row < rowCount; row++)
			{
				for (int column = 0; column < columnCount; column++)
				{
					sumMatrix[row, column] = matrixA[row, column] + matrixB[row, column];
				}
			}

			return sumMatrix;
		}
		
		private static int[,] GenerateRandomValueMatrix(int rowCount, int columnCount, int startValueRange, int endValueRange)
		{
			int[,] matrix = new int[rowCount, columnCount];

			for (int i = 0; i < rowCount; i++)
			{
				for (int j = 0; j < columnCount; j++)
				{
					matrix[i, j] = Random.Next(startValueRange, endValueRange + 1);
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
	}
}
