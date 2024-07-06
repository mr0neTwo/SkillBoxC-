namespace Models.Animals;

public sealed class Bird : Animal
{
	public Bird()
	{
	}
	
	public Bird(DatabaseEF.Entities.Animal dbAnimal) : base(dbAnimal)
	{
	}

	public Bird(int id, string name, int age, float weight) : base(id, name, age, weight)
	{
	}

	public override void Feed(string food)
	{
		Console.WriteLine($"The {Name} ate the {food}");
	}
}
