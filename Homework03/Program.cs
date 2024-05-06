using System;

namespace Homework03
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			// Task01();
			// Task02();
			// Task03();
			// Task04();
			Task05();
		}

		private static void Task01()
		{
			int number = GetIntFromConsole("Enter an integer: ");
			bool isEven = ParityCheck(number);
			string result = isEven ? "even" : "odd";
			Console.WriteLine($"Number {number} is {result}");
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

		private static bool ParityCheck(int number)
		{
			return number % 2 == 0;
		}

		private static void Task02()
		{
			int cardSum = CalculateCardSum();
			Console.WriteLine($"Card sum: {cardSum}");
		}

		private static int CalculateCardSum()
		{
			int sum = 0;
			int numberOfCard = GetIntFromConsole("Enter the number of cards: ");

			for (int i = 0; i < numberOfCard; i++)
			{
				bool incorrectNumber = true;
				
				while (incorrectNumber)
				{
					Console.Write($"Enter value of {i + 1} card: ");
					string input = Console.ReadLine();

					switch (input)
					{
						case "J":
						case "Q":
						case "K":
						case "T":
						{
							sum += 10;
							incorrectNumber = false;
							
							break;
						}
						default:
						{
							if (int.TryParse(input, out int number) && number > 0 && number < 11)
							{
								sum += number;
								incorrectNumber = false;
							}
							else
							{
								Console.WriteLine("Please enter correct value of card. (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, or T)");
								incorrectNumber = true;
							}
							
							break;
						}
					}
				}
			}

			return sum;
		}

		private static void Task03()
		{
			int number = GetIntFromConsole("Enter an integer: ");
			bool isPrime = CheckPrimeNumber(number);
			string result = isPrime ? "prime" : "not prime";
			Console.WriteLine($"Number {number} is {result}");
		}

		private static bool CheckPrimeNumber(int number)
		{
			if (number == 0)
			{
				return false;
			}

			if (number < 0)
			{
				number = -number;
			}

			if (number == 1)
			{
				return true;
			}
			
			int counter = 2;

			while (counter < number)
			{
				if (number % counter == 0)
				{
					return false;
				}

				counter++;
			}

			return true;
		}

		private static void Task04()
		{
			int lenght = GetIntFromConsole("Enter lenght of sequence: ");
			int minValue = int.MaxValue;

			for (int i = 0; i < lenght; i++)
			{
				int value = GetIntFromConsole($"Enter an {i + 1} integer: ");

				if (value < minValue)
				{
					minValue = value;
				}
			}
			
			Console.WriteLine($"Min value of sequence is {minValue}");
		}

		private static void Task05()
		{
			int maxValue;

			do
			{
				maxValue = GetIntFromConsole("Enter max value of range: ");

				if (maxValue <= 0)
				{
					Console.WriteLine("The number must be positive");
				}
			} while (maxValue <= 0);

			Random random = new Random();
			int randomNumber = random.Next(maxValue + 1);
			bool exitRequested = false;

			while (!exitRequested)
			{
				int guess = 0;
				
				while (true)
				{
					Console.Write("Try to guess a random number or press Enter to exit: ");
					string input = Console.ReadLine();

					if (string.IsNullOrEmpty(input))
					{
						exitRequested = true;
						Console.WriteLine($"Random number is {randomNumber}");
						
						break;
					}
				
					if (int.TryParse(input, out guess))
					{
						break;
					}
				
					Console.WriteLine("Entered value is not integer");
				}

				if (guess == randomNumber)
				{
					Console.WriteLine("Congratulations, you guessed it!!!");
					
					break;
				}

				string keyWord = guess < randomNumber ? "less" : "more";
				Console.WriteLine($"Number {guess} is {keyWord} then random number");
			}
		}
	}
}
