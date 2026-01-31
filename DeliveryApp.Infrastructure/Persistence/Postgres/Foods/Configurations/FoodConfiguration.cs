using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Foods.Configurations;

public class FoodConfiguration : EntityTypeConfiguration<Food>
{
    public override void Configure(EntityTypeBuilder<Food> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Name).HasMaxLength(250).IsRequired();
        builder.Property(u => u.Description).HasMaxLength(1000);

        builder.Property(f => f.Active).IsRequired();
        builder.Property(f => f.Price).IsRequired();

        builder.HasMany(f => f.Tags)
            .WithMany(t => t.Foods)
            .UsingEntity<Dictionary<string, object>>(
                "FoodTags",
                tag =>
                    tag.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                food =>
                    food.HasOne<Food>().WithMany().HasForeignKey("FoodId"));

        builder.HasOne(f => f.FoodImage)
            .WithOne(f => f.Food);

        builder.HasOne(f => f.FoodCategory).WithMany()
            .HasForeignKey(f => f.FoodCategoryId);
    }
}