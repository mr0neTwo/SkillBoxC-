namespace Models.Animals;

public sealed class Amphibian : Animal
{
	public Amphibian()
	{
	}
	
	public Amphibian(DatabaseEF.Entities.Animal dbAnimal) : base(dbAnimal)
	{
	}
	
	public Amphibian(int id, string name, int age, float weight) : base(id, name, age, weight)
	{
	}

	public override void Feed(string food)
	{
		Console.WriteLine($"The {Name} ate the {food}");
	}
}
