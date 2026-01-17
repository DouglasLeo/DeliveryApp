using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Orders.Configurations;

public class OrderConfiguration : EntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        
        builder.Property(o => o.OrderStatus).IsRequired();
        
        builder.HasMany(o => o.OrderItems)
            .WithOne(i => i.Order)
            .HasForeignKey(i => i.OrderId);
        
        builder.Property(o => o.Street).HasMaxLength(150).IsRequired();
        builder.Property(o => o.City).HasMaxLength(150).IsRequired();
        builder.Property(o => o.Country).HasMaxLength(100).IsRequired();
        builder.Property(o => o.HouseNumber).HasMaxLength(10).IsRequired();
        builder.Property(o => o.Neighboorhood).HasMaxLength(150).IsRequired();
        builder.Property(o => o.PostalCode).HasMaxLength(8).IsRequired();
        builder.Property(o => o.Reference).HasMaxLength(250).IsRequired();
        builder.Property(o => o.Complement).HasMaxLength(300).IsRequired();
    }
}