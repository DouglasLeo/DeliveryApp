using DeliveryApp.Domain.Entities.Order;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Orders.Configurations;

public class OrderItemConfiguration : EntityTypeConfiguration<OrderItems>
{
    public override void Configure(EntityTypeBuilder<OrderItems> builder)
    {
        base.Configure(builder);

        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.UnitPrice).IsRequired();
    }
}