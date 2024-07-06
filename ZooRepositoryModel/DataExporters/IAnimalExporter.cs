using Models.Animals;

namespace Models.DataExporters;

public interface IAnimalExporter
{
	void Export(Animal[] animals);
}
