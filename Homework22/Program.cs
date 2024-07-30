using Homework22;

bool isCompleted = false;

while (!isCompleted)
{
	string? input = Console.ReadLine();

	try
	{
		double number = Parser.StringToDouble(input);
		Console.WriteLine($"The conversion of the number {number} has been completed successfully");
		isCompleted = true;
	}
	catch (ArgumentNullException)
	{
		Console.WriteLine("You entered an empty number, try again");
	}
	catch (FormatException formatException)
	{
		Console.WriteLine(formatException.Message);
		Console.WriteLine("Try again");
	}
}
