using DatabasePN.Entities;
using DatabasePN.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace DatabasePN;

public class DatabaseContext : DbContext
{
	public DbSet<Note> Notes { get; set; }
	
	public DatabaseContext(DbContextOptions<DatabaseContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new NoteConfiguration());
	}
	
	public void FillDefaultValues()
	{
		Database.EnsureDeleted();
		Database.EnsureCreated();

		foreach (Note note in DefaultValues.DefaultValues.Notes())
		{
			Notes.Add(note);
		}
		
		SaveChanges();
	}
}
