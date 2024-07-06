namespace Models.Animals;

public sealed class Mammal : Animal
{
	public Mammal()
	{
	}
	
	public Mammal(DatabaseEF.Entities.Animal dbAnimal) : base(dbAnimal)
	{
	}
	
	public Mammal(int id, string name, int age, float weight) : base(id, name, age, weight)
	{
	}

	public override void Feed(string food)
	{
		Console.WriteLine($"The {Name} ate the {food}");
	}
}
