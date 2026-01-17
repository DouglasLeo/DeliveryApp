using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Users.Configurations;

public class RoleConfiguration : EntityTypeConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(u => u.Name).HasMaxLength(250).IsRequired();
    }
}