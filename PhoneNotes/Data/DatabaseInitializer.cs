using System.Security.Claims;
using DatabasePN;
using DatabasePN.DefaultValues;
using Microsoft.AspNetCore.Identity;
using PhoneNoteApplication.Data;
using PhoneNoteApplication.Models;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace PhoneNotes.Data;

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

	public static async Task Rebuild(DatabaseContext context, UserManager<User> userManager,
									 RoleManager<Role> roleManager)
	{
		await context.Database.EnsureDeletedAsync();
		await context.Database.EnsureCreatedAsync();

		await context.Notes.AddRangeAsync(DefaultValues.Notes());
		await CreateUsersAndRoles(userManager, roleManager);

		await context.SaveChangesAsync();
	}

	private static async Task CreateUsersAndRoles(UserManager<User> userManager, RoleManager<Role> roleManager)
	{
		Role adminRole = new() { Name = Roles.Admin, };
		await roleManager.CreateAsync(adminRole);

		Claim[] adminClaims =
		{
			new(ClaimTypes.Role, AppClaims.CanViewNote),
			new(ClaimTypes.Role, AppClaims.CanAddNote),
			new(ClaimTypes.Role, AppClaims.CanEditNote),
			new(ClaimTypes.Role, AppClaims.CanDeleteNote),
			new(ClaimTypes.Role, AppClaims.CanViewUsers),
			new(ClaimTypes.Role, AppClaims.CanAddUser),
			new(ClaimTypes.Role, AppClaims.CanEditUser),
			new(ClaimTypes.Role, AppClaims.CanDeleteUser)
		};

		foreach (Claim claim in adminClaims)
		{
			await roleManager.AddClaimAsync(adminRole, claim);
		}

		Role userRole = new() { Name = Roles.User };
		await roleManager.CreateAsync(userRole);


		Claim[] userClaims =
		{
			new(ClaimTypes.Role, AppClaims.CanViewNote),
			new(ClaimTypes.Role, AppClaims.CanAddNote),
			new(ClaimTypes.Role, AppClaims.CanEditNote),
			new(ClaimTypes.Role, AppClaims.CanDeleteNote)
		};

		foreach (Claim claim in userClaims)
		{
			await roleManager.AddClaimAsync(userRole, claim);
		}
		
		User admin = new() { UserName = "Admin", Email = "admin@example.com"};

		await userManager.CreateAsync(admin, "123456");
		await userManager.AddToRoleAsync(admin, Roles.Admin);

		User user = new() { UserName = "JohnDoe", Email = "johndoe@example.com" };

		await userManager.CreateAsync(user, "123456");
		await userManager.AddToRoleAsync(user, Roles.User);
	}
}
