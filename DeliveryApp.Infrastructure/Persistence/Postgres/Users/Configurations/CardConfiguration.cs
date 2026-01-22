using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Configurations;

public class CardConfiguration : EntityTypeConfiguration<Card>
{
    public override void Configure(EntityTypeBuilder<Card> builder)
    {
        base.Configure(builder);
        
        builder.Property(c => c.Token).HasMaxLength(100).IsRequired();
        builder.Property(c => c.CardBrand).IsRequired();
        builder.Property(c => c.CardType).IsRequired();
        builder.Property(c => c.CardFinalNumbers).IsRequired().HasMaxLength(4);
    }
}