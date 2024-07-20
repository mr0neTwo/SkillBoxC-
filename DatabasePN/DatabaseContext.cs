using DatabasePN.EntityConfigs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneNoteApplication.Models;

namespace DatabasePN;

public class DatabaseContext : IdentityDbContext<User, Role, int>
{
	public DbSet<Note> Notes { get; set; }
	
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		
		modelBuilder.ApplyConfiguration(new NoteConfiguration());
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new RoleConfiguration());
	}
}
