using Snipe.App.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Snipe.Infrastructure.Persistence.Events.Configurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> entityType)
        {
            entityType.HasKey(x => x.EventId);
        }
    }
}
