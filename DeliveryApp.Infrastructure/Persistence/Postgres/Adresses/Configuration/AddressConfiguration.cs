using DeliveryApp.Domain.Entities.Address;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Adresses.Configuration;

public class AddressConfiguration : EntityTypeConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        
        builder.Property(f => f.HouseNumber).HasMaxLength(10).IsRequired();
        builder.Property(f => f.Street).HasMaxLength(150).IsRequired();
        builder.Property(f => f.City).HasMaxLength(150).IsRequired();
        builder.Property(f => f.Neighboorhood).HasMaxLength(150).IsRequired();
        builder.Property(f => f.Country).HasMaxLength(100).IsRequired();
        builder.Property(f => f.PostalCode).HasMaxLength(8).IsRequired();
        builder.Property(f => f.Complement).HasMaxLength(300);
        builder.Property(f => f.Reference).HasMaxLength(250);
        
        builder.HasIndex(f => f.UserId);
    }
}