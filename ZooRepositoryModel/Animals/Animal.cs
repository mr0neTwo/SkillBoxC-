namespace Models.Animals;

public abstract class Animal
{
	public int Id { get; }

	public string Name { get; }

	public int Age { get; }

	public float Weight { get; }

	protected Animal()
	{
	}

	protected Animal(DatabaseEF.Entities.Animal dbAnimal)
	{
		Id = dbAnimal.Id;
		Name = dbAnimal.Name;
		Age = dbAnimal.Age;
		Weight = dbAnimal.Weight;
	}

	protected Animal(int id, string name, int age, float weight)
	{
		Id = id;
		Name = name;
		Age = age;
		Weight = weight;
	}
	
	public abstract void Feed(string food);

	public override string ToString()
	{
		return $"[Animal: {Name}]";
	}
}