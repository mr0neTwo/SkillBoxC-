using DatabaseEF;
using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Animals;
using Models.Factory;
using Animal = Models.Animals.Animal;

namespace Models.Models;

public sealed class ZooRepository : IAnimalDataProvider
{
	private readonly DatabaseContext _database;

	public ZooRepository()
	{
		_database = new DatabaseContext();
		_database.FillDefaultValues();
	}

	public Animal[] GetAllAnimal()
	{
		List<Animal> animals = new();
		DatabaseEF.Entities.Animal[] dbAnimals = _database.Animals.AsNoTracking().ToArray();

		foreach (DatabaseEF.Entities.Animal dbAnimal in dbAnimals)
		{
			Animal animal = AnimalFactory.Create(dbAnimal);
			animals.Add(animal);
		}

		return animals.ToArray();
	}

	public void AddAnimal(Animal animal)
	{
		DatabaseEF.Entities.Animal dbAnimal = CreateDbAnimal(animal);
		_database.Animals.Add(dbAnimal);
		_database.SaveChanges();
	}

	public void UpdateAnimal(Animal animal)
	{
		DatabaseEF.Entities.Animal? dbAnimal = _database.Animals.FirstOrDefault(a => a.Id == animal.Id);

		if (dbAnimal == null)
		{
			return;
		}

		dbAnimal.Name = animal.Name;
		dbAnimal.Age = animal.Age;
		dbAnimal.Weight = animal.Weight;
		dbAnimal.AnimalType = GetAnimalType(animal);
		
		_database.Animals.Update(dbAnimal);
		_database.SaveChanges();
	}
	
	public void DeleteAnimal(Animal animal)
	{
		DatabaseEF.Entities.Animal dbAnimal = CreateDbAnimal(animal);
		_database.Animals.Remove(dbAnimal);
		_database.SaveChanges();
	}
	

	private DatabaseEF.Entities.Animal CreateDbAnimal(Animal animal)
	{
		DatabaseEF.Entities.Animal dbAnimal = new()
		{
			Id = animal.Id,
			Name = animal.Name,
			Age = animal.Age,
			Weight = animal.Weight,
			AnimalType = GetAnimalType(animal)
		};

		return dbAnimal;
	}


	private AnimalType GetAnimalType(Animal animal)
	{
		return animal switch
		{
			Mammal => AnimalType.Mammal,
			Bird => AnimalType.Bird,
			Amphibian => AnimalType.Amphibian,
			_ => AnimalType.None
		};
	}
}
