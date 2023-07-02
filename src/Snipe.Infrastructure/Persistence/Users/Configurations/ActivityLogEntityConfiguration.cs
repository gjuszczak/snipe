using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Snipe.App.Features.Users.Entities;

namespace Snipe.Infrastructure.Persistence.Users.Configurations
{
    public class ActivityLogEntityConfiguration : IEntityTypeConfiguration<ActivityLogEntity>
    {
        public void Configure(EntityTypeBuilder<ActivityLogEntity> entityType)
        {
            entityType.HasKey(al => al.Id);
            entityType.Property(al => al.Kind).HasConversion<string>();
            entityType.Property(al => al.CreatedByIp).HasMaxLength(256);
            entityType.Property(al => al.CreatedByUserAgent).HasMaxLength(1024);
        }
    }
}
