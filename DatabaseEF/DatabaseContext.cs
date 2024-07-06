using DatabaseEF.Entities;
using DatabaseEF.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DatabaseEF;

public sealed class DatabaseContext : DbContext
{
	public DbSet<Client> Clients { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Animal> Animals { get; set; }
	
	public DatabaseContext()
	{
		Database.EnsureCreated();
	}
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		NpgsqlConnectionStringBuilder postgresConnectionStringBuilder = new()
		{
			Host = "localhost",
			Port = 5432,
			Database = "skillbox_test",
			Username = "postgres",
			Password = "225567",
			Pooling = true
		};

		optionsBuilder.UseNpgsql(postgresConnectionStringBuilder.ConnectionString);
		optionsBuilder.LogTo(Console.WriteLine);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new ClientConfiguration());
		modelBuilder.ApplyConfiguration(new OrderConfiguration());
		modelBuilder.ApplyConfiguration(new AnimalConfiguration());
	}

	public void FillDefaultValues()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();

		foreach (Client client in DefaultValues.Сlients)
		{
			Clients.Add(client);
		}
		
		foreach (Order order in DefaultValues.Orders)
		{
			Orders.Add(order);
		}
		
		foreach (Animal animal in DefaultValues.Animals)
		{
			Animals.Add(animal);
		}

		SaveChanges();
	}
}
