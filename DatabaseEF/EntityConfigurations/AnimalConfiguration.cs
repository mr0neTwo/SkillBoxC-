using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseEF.EntityConfigurations;

public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
	public void Configure(EntityTypeBuilder<Animal> builder)
	{
		builder.HasKey(animal => animal.Id);
		builder.Property(animal => animal.Name).HasMaxLength(20);
	}
}
