using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snipe.App.Features.Users.Entities;

namespace Snipe.Infrastructure.Persistence.App.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> entityType)
        {
            entityType.HasKey(u => u.Id);
            entityType.HasIndex(u => u.Email).IsUnique();
            entityType.OwnsMany(u => u.RefreshTokens, rt =>
            {
                rt.WithOwner().HasForeignKey("UserId");
                rt.HasKey(rt => rt.Id);
                rt.HasIndex(rt => rt.Token).IsUnique();
            });
        }
    }
}
