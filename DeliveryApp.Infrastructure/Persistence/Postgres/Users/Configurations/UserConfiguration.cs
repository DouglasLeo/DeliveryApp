using DeliveryApp.Domain.Entities.User;
using DeliveryApp.Infrastructure.Persistence.Postgres.Shared.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryApp.Infrastructure.Persistence.Postgres.Users.Configurations;

public class UserConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        
        builder.Property(u => u.Name).HasMaxLength(250).IsRequired();
        builder.Property(u => u.Email).HasMaxLength(250).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(100).IsUnicode(false).IsRequired();
        builder.Property(u => u.Active).IsRequired();
        
        builder.HasMany(u => u.Orders).
            WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        builder.HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        builder.HasOne(u => u.MainAddress)
            .WithOne()
            .HasForeignKey<User>(u => u.AddressId);
        
        builder.HasMany(u => u.Cards)
            .WithOne(c => c.User)
            .HasForeignKey(u => u.UserId);
        
        builder.HasOne(u => u.Card)
            .WithOne()
            .HasForeignKey<User>(u => u.CardId);
        
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role =>
                    role.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                user =>
                    user.HasOne<User>().WithMany().HasForeignKey("UserId"));
        
        builder.HasIndex(u => u.Email).IsUnique();
    }
}