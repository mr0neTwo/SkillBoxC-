using System.Xml.Serialization;
using Models.Animals;

namespace Models.DataExporters;

public sealed class XmlAnimalExporter : IAnimalExporter
{
	public void Export(Animal[] animals)
	{
		XmlSerializer xmlSerializer = new(typeof(Animal[]), [typeof(Amphibian), typeof(Mammal), typeof(Bird), typeof(UnknownAnimal)]);

		using (var writer = new StreamWriter("animals.xml"))
		{
			xmlSerializer.Serialize(writer, animals);
		}
	}
}
