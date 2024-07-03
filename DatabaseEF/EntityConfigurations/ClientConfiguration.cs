using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseEF.EntityConfigurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
	public void Configure(EntityTypeBuilder<Client> builder)
	{
		builder.HasKey(client => client.Id);
		builder.Property(client => client.FirstName).HasMaxLength(20);
		builder.Property(client => client.LastName).HasMaxLength(20);
		builder.Property(client => client.ThirdName).HasMaxLength(20);
		builder.Property(client => client.PhoneNumber).HasMaxLength(20);
		builder.Property(client => client.Email).IsRequired().HasMaxLength(20);

		builder
			.HasMany(client => client.Orders)
			.WithOne(order => order.Client);
	}
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasKey(order => order.Id);
		builder.Property(client => client.ProductName).HasMaxLength(20);
		builder.Property(client => client.ProductCode);
		builder.Property(client => client.Email).IsRequired().HasMaxLength(20);

		builder
			.HasOne(order => order.Client)
			.WithMany(client => client.Orders)
			.HasForeignKey(order => order.ClientId);
	}
}
