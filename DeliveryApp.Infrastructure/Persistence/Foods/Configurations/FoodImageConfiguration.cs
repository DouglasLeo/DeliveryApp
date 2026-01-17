using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Configurations;

public class FoodImageConfiguration : EntityTypeConfiguration<FoodImage>
{
    public override void Configure(EntityTypeBuilder<FoodImage> builder)
    {
        base.Configure(builder);
        
        builder.Property(f => f.ImageUrl).IsRequired();
    }
}