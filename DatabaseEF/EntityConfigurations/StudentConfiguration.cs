using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseEF.EntityConfigurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
	public void Configure(EntityTypeBuilder<Student> builder)
	{
		builder.HasKey(student => student.Id);
		builder.Property(student => student.Name).HasMaxLength(20);
		builder.Property(student => student.Email).HasMaxLength(20);
		builder.Property(student => student.Phone).HasMaxLength(20);
		builder.Property(student => student.Subscribe);
	}
}
