using DatabasePN;
using DatabasePN.DefaultValues;
using PhoneNoteApplication.Models;

namespace PhoneNoteAuthJWT.Data;

public static class DatabaseInitializer
{
	public static void Initialize(DatabaseContext context)
	{
		context.Database.EnsureCreated();

		if (context.Notes.Any())
		{
			return;
		}

		foreach (Note note in DefaultValues.Notes())
		{
			context.Notes.Add(note);
		}

		context.SaveChanges();
	}

	public static async Task Rebuild(DatabaseContext context)
	{
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();

		await context.Notes.AddRangeAsync(DefaultValues.Notes());
		await CreateUsers(context);

		await context.SaveChangesAsync();
	}

	private static async Task CreateUsers(DatabaseContext context)
	{
		User admin = new()
		{
			UserName = "Admin",
			PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
			Email = "admin@example.com"
		};
		
		User user = new()
		{
			UserName = "JohnDoe",
			PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
			Email = "johndoe@example.com"
		};

		context.Users.Add(admin);
		context.Users.Add(user);

		await context.SaveChangesAsync();
	}
}
