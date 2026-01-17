using DeliveryApp.Domain.Entities.Food;
using DeliveryApp.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Foods.Configurations;

public class TagConfiguration : EntityTypeConfiguration<Tag>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);
        
        builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
    }
}