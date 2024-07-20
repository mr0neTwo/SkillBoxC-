using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhoneNoteApplication.Models;

namespace DatabasePN.EntityConfigs;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(user => user.Id);
		builder.Property(user => user.UserName).HasMaxLength(20).IsRequired();
		builder.Property(user => user.Email).HasMaxLength(20);
	}
}
