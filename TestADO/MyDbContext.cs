using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;

namespace TestADO
{
	public class MyDbContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		// {
		// 	optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyDatabase;Trusted_Connection=True;");
		// }

		
		
		public static class PasswordHasher
		{
			public static string HashPassword(string password, byte[] salt)
			{
				const int iterations = 10000;
				const int hashByteSize = 20;

				var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
				var hash = pbkdf2.GetBytes(hashByteSize);

				var hashBytes = new byte[hashByteSize + salt.Length];
				Array.Copy(salt, 0, hashBytes, 0, salt.Length);
				Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

				return Convert.ToBase64String(hashBytes);
			}
		}
	}
	
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<Order> Order { get; set; }
	}

	public class Order

	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public decimal Total { get; set; }
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
	}
}
