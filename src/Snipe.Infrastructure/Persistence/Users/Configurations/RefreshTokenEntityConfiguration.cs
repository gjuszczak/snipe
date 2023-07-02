using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snipe.App.Features.Users.Entities;

namespace Snipe.Infrastructure.Persistence.Users.Configurations
{
    public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntity> entityType)
        {
            entityType.HasKey(rt => rt.Id);
            entityType.HasIndex(rt => rt.Token).IsUnique();
            entityType.Property(rt => rt.CreatedByIp).HasMaxLength(256);
            entityType.Property(rt => rt.CreatedByUserAgent).HasMaxLength(1024);
            entityType.OwnsOne(rt => rt.RevokeDetails, rd =>
            {
                rd.ToTable("RefreshTokenRevokeDetails");
                rd.Property(x => x.RevokedByIp).HasMaxLength(256);
                rd.Property(x => x.RevokedByUserAgent).HasMaxLength(1024);
            });
        }
    }
}
