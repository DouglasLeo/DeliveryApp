using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Configurations;

public class RoleConfiguration : EntityTypeConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);
        builder.Property(u => u.Name).HasMaxLength(250).IsRequired();
    }
}