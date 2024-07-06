using System.Text.Json;
using System.Text.Json.Serialization;
using Models.Animals;

namespace Models.DataExporters;

public sealed class JsonAnimalExporter : IAnimalExporter
{
	public void Export(Animal[] animals)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true,                         
			Converters = { new JsonStringEnumConverter() }
		};

		string json = JsonSerializer.Serialize(animals, options);
		File.WriteAllText("animals.json", json);
	}
}