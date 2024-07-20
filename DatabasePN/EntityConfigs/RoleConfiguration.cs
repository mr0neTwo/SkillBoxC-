using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneNoteApplication.Models;

namespace DatabasePN.EntityConfigs;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.HasKey(role => role.Id);
		builder.Property(role => role.Name).HasMaxLength(20).IsRequired();
	}
}
