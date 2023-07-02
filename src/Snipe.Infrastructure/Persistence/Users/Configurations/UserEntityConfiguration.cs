using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snipe.App.Features.Users.Entities;

namespace Snipe.Infrastructure.Persistence.Users.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entityType)
        {
            entityType.HasKey(u => u.Id);
            entityType.Property(u => u.Email).HasMaxLength(256);
            entityType.Property(u => u.NormalizedEmail).HasMaxLength(256);
            entityType.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            entityType.HasIndex(u => u.NormalizedEmail).IsUnique();
            entityType.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
        }
    }
}
