namespace DatabaseEF.Entities;

public sealed class Animal
{
	public static Animal Unknown => new() { Name = "Unknown", AnimalType = AnimalType.None };
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int Age { get; set; }
	public float Weight { get; set; }
	public AnimalType AnimalType { get; set; }
}
