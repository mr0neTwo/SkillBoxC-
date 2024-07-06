using System.Text;
using DatabaseEF;
using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestEF;

public static class Program
{
	public static void Main(string[] args)
	{
		DatabaseContext database = new();
		
		database.FillDefaultValues();

		List<Client> clients = database.Clients.Include(client => client.Orders).ToList();
		List<Order> orders = database.Orders.Include(order => order.Client).ToList();
		List<Animal> animals = database.Animals.ToList();

		StringBuilder clientsResult = new();

		clientsResult.Append("Clients:\n");
		
		foreach (Client client in clients)
		{
			clientsResult.Append($"{client.FirstName,-10}");
			clientsResult.Append($"{client.LastName,-10}");
			clientsResult.Append($"{client.ThirdName,-10}");
			clientsResult.Append($"{client.PhoneNumber,-20}");
			clientsResult.Append($"{client.Email,-20}");
			clientsResult.Append($"{client.Orders.Count,-10}");
			clientsResult.Append("\n");
		}

		Console.WriteLine(clientsResult.ToString());
		Console.WriteLine();
		
		StringBuilder ordersResult = new();

		ordersResult.Append("Orders:\n");

		foreach (Order order in orders)
		{
			ordersResult.Append($"{order.ProductName,-22}");
			ordersResult.Append($"{order.ProductCode,-6}");
			ordersResult.Append($"{order.Email,-20}");
			ordersResult.Append($"{order.Client.FirstName,-10}");
			ordersResult.Append($"{order.Client.LastName,-10}");
			ordersResult.Append($"{order.Client.PhoneNumber,-15}");
			ordersResult.Append("\n");
		}

		Console.WriteLine(ordersResult.ToString());
		Console.WriteLine();
		
		StringBuilder animalsResult = new();

		animalsResult.Append("Animals:\n");

		foreach (Animal animal in animals)
		{
			animalsResult.Append($"{animal.Name,-12}");
			animalsResult.Append($"{animal.Age,-6}");
			animalsResult.Append($"{animal.Weight,-6}");
			animalsResult.Append($"{animal.AnimalType,-10}");
			animalsResult.Append("\n");
		}

		Console.WriteLine(animalsResult.ToString());
	}
}