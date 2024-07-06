namespace Models.Animals;

public sealed class UnknownAnimal() : Animal(DatabaseEF.Entities.Animal.Unknown)
{
	public override void Feed(string food)
	{
		Console.WriteLine($"Unknown animal ate {food}");
	}
}
