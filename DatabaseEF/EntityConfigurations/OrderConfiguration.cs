using DatabaseEF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseEF.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasKey(order => order.Id);
		builder.Property(order => order.ProductName).HasMaxLength(20);
		builder.Property(order => order.ProductCode);
		builder.Property(order => order.Email).IsRequired().HasMaxLength(20);

		builder
			.HasOne(order => order.Client)
			.WithMany(client => client.Orders)
			.HasForeignKey(order => order.ClientId);
	}
}