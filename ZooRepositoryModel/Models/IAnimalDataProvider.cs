using Animal = Models.Animals.Animal;

namespace Models.Models;

public interface IAnimalDataProvider
{
	Animal[] GetAllAnimal();

	void AddAnimal(Animal animal);

	void UpdateAnimal(Animal animal);

	void DeleteAnimal(Animal animal);
}
