using DatabaseEF.Entities;

namespace DatabaseEF;

public static class DefaultValues
{
	public static readonly Order[] Orders =
	{
		new() { ProductCode = 5674, ClientId = 1, ProductName = "Холодильник", Email = "alice@example.com" },
		new() { ProductCode = 8493, ClientId = 2, ProductName = "Телевизор", Email = "bob@example.com" },
		new() { ProductCode = 2318, ClientId = 3, ProductName = "Ноутбук", Email = "charlie@example.com" },
		new() { ProductCode = 4571, ClientId = 4, ProductName = "Смартфон", Email = "david@example.com" },
		new() { ProductCode = 1290, ClientId = 1, ProductName = "Стиральная машина", Email = "alice@example.com" },
		new() { ProductCode = 3452, ClientId = 5, ProductName = "Микроволновка", Email = "emily@example.com" },
		new() { ProductCode = 6789, ClientId = 6, ProductName = "Пылесос", Email = "frank@example.com" },
		new() { ProductCode = 9087, ClientId = 2, ProductName = "Кондиционер", Email = "bob@example.com" },
		new() { ProductCode = 6574, ClientId = 7, ProductName = "Принтер", Email = "hannah@example.com" },
		new() { ProductCode = 8234, ClientId = 1, ProductName = "Фен", Email = "isaac@example.com" },
		new() { ProductCode = 3721, ClientId = 1, ProductName = "Утюг", Email = "alice@example.com" },
		new() { ProductCode = 5418, ClientId = 6, ProductName = "Кофеварка", Email = "frank@example.com" },
		new() { ProductCode = 9873, ClientId = 1, ProductName = "Посудомоечная машина", Email = "grace@example.com" },
		new() { ProductCode = 6510, ClientId = 7, ProductName = "Настольная лампа", Email = "hannah@example.com" },
		new() { ProductCode = 7834, ClientId = 2, ProductName = "Тостер", Email = "bob@example.com" },
		new() { ProductCode = 4298, ClientId = 1, ProductName = "Электрический чайник", Email = "isaac@example.com" },
		new() { ProductCode = 3190, ClientId = 3, ProductName = "Блендер", Email = "charlie@example.com" },
		new() { ProductCode = 6472, ClientId = 4, ProductName = "Плита", Email = "david@example.com" },
		new() { ProductCode = 8753, ClientId = 7, ProductName = "Плеер", Email = "hannah@example.com" },
		new() { ProductCode = 2908, ClientId = 6, ProductName = "Электромясорубка", Email = "frank@example.com" }
	}; 

	public static readonly Client[] Сlients =
	{
		new() { FirstName = "Alex", LastName = "Johnson", ThirdName = "Michael", PhoneNumber = "+7 (123) 456-78-90", Email = "alice@example.com" },
		new() { FirstName = "Bob", LastName = "Smith", ThirdName = "Lee", PhoneNumber = "+7 (234) 567-89-01", Email = "bob@example.com" },
		new() { FirstName = "Charlie", LastName = "Brown", ThirdName = "Andrew", PhoneNumber = "+7 (345) 678-90-12", Email = "charlie@example.com" },
		new() { FirstName = "David", LastName = "Davis", ThirdName = "Robert", PhoneNumber = "+7 (456) 789-01-23", Email = "david@example.com" },
		new() { FirstName = "Emily", LastName = "Taylor", ThirdName = "Grace", PhoneNumber = "+7 (567) 890-12-34", Email = "emily@example.com" },
		new() { FirstName = "Frank", LastName = "Johnson", ThirdName = "Henry", PhoneNumber = "+7 (678) 901-23-45", Email = "frank@example.com" },
		new() { FirstName = "Hannah", LastName = "Wilson", ThirdName = "Elizabeth", PhoneNumber = "+7 (789) 012-34-56", Email = "hannah@example.com" }
	};
	
	public static readonly Animal[] Animals =
	{
		new() { Name = "Elephant", Age = 10, Weight = 5000, AnimalType = AnimalType.Mammal },
		new() { Name = "Tiger", Age = 5, Weight = 300, AnimalType = AnimalType.Mammal },
		new() { Name = "Parrot", Age = 2, Weight = 1, AnimalType = AnimalType.Bird },
		new() { Name = "Frog", Age = 1, Weight = 0.5f, AnimalType = AnimalType.Amphibian },
		new() { Name = "Penguin", Age = 4, Weight = 15, AnimalType = AnimalType.Bird },
		new() { Name = "Lion", Age = 8, Weight = 250, AnimalType = AnimalType.Mammal },
		new() { Name = "Dolphin", Age = 12, Weight = 400, AnimalType = AnimalType.Mammal },
		new() { Name = "Eagle", Age = 6, Weight = 7, AnimalType = AnimalType.Bird },
		new() { Name = "Shark", Age = 20, Weight = 800, AnimalType = AnimalType.Mammal },
		new() { Name = "Rabbit", Age = 3, Weight = 2, AnimalType = AnimalType.Mammal },
		new() { Name = "Hawk", Age = 5, Weight = 6, AnimalType = AnimalType.Bird },
		new() { Name = "Salamander", Age = 4, Weight = 1, AnimalType = AnimalType.Amphibian },
		new() { Name = "Whale", Age = 25, Weight = 15000, AnimalType = AnimalType.Mammal },
		new() { Name = "Pigeon", Age = 2, Weight = 0.3f, AnimalType = AnimalType.Bird },
		new() { Name = "Koala", Age = 7, Weight = 10, AnimalType = AnimalType.Mammal },
		new() { Name = "Turtle", Age = 50, Weight = 300, AnimalType = AnimalType.Amphibian },
		new() { Name = "Gecko", Age = 3, Weight = 0.1f, AnimalType = AnimalType.Amphibian },
		new() { Name = "Otter", Age = 6, Weight = 12, AnimalType = AnimalType.Mammal },
		new() { Name = "Owl", Age = 3, Weight = 1.5f, AnimalType = AnimalType.Bird },
		new() { Name = "Toad", Age = 2, Weight = 0.8f, AnimalType = AnimalType.Amphibian }
	};
}
