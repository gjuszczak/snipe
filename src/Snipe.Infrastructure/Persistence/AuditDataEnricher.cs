﻿using Snipe.App.Core.Entities;
using Snipe.App.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Snipe.Infrastructure.Persistence
{
    public class AuditDataEnricher : IAuditDataEnricher
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public AuditDataEnricher(ICorrelationIdProvider correlationIdProvider)
        {
            _correlationIdProvider = correlationIdProvider;
        }

        public void Enrich(DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _correlationIdProvider.GetCorrelationId().ToString();
                        entry.Entity.Created = DateTimeOffset.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _correlationIdProvider.GetCorrelationId().ToString();
                        entry.Entity.LastModified = DateTimeOffset.UtcNow;
                        break;
                }
            }
        }
    }
}
