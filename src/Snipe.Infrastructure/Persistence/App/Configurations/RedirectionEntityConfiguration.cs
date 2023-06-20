﻿using Snipe.App.Features.Redirections.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Snipe.Infrastructure.Persistence.App.Configurations
{
    public class RedirectionEntityConfiguration : IEntityTypeConfiguration<RedirectionEntity>
    {
        public void Configure(EntityTypeBuilder<RedirectionEntity> entityType)
        {
            entityType.HasKey(o => o.Id);
            entityType.HasIndex(e => e.Name).IsUnique();
        }
    }
}
