using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Configurations;

public class FoodCategoryConfiguration : EntityTypeConfiguration<FoodCategory>
{
    public override void Configure(EntityTypeBuilder<FoodCategory> builder)
    {
        base.Configure(builder);
        
        builder.Property(f => f.Name).HasMaxLength(150).IsRequired();
    }
}