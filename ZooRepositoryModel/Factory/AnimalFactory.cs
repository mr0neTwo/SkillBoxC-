using DatabaseEF.Entities;
using Models.Animals;
using Animal = Models.Animals.Animal;

namespace Models.Factory;

public static class AnimalFactory
{
	public static Animal Create(DatabaseEF.Entities.Animal dbAnimal)
	{
		return dbAnimal.AnimalType switch
		{
			AnimalType.Mammal => new Mammal(dbAnimal),
			AnimalType.Bird => new Bird(dbAnimal),
			AnimalType.Amphibian => new Amphibian(dbAnimal),
			_ => new UnknownAnimal()
		};
	}

	public static Animal Create(int id, string name, int age, float weight, AnimalType animalType)
	{
		return animalType switch
		{
			AnimalType.Mammal => new Mammal(id, name, age, weight),
			AnimalType.Bird => new Bird(id, name, age, weight),
			AnimalType.Amphibian => new Amphibian(id, name, age, weight),
			_ => new UnknownAnimal()
		};
	}
}