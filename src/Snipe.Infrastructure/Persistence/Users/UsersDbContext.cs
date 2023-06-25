﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using Snipe.App.Features.Users.Entities;
using Snipe.App.Features.Users.Services;

namespace Snipe.Infrastructure.Persistence.App
{
    // Add-Migration Initial -c UsersDbContext -o Persistence/Users/Migrations
    // docker run --name postgresdb -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres:15-alpine
    // docker run --name pgadmin4 -p 8080:80 -e 'PGADMIN_DEFAULT_EMAIL=admin@admin.com' -e 'PGADMIN_DEFAULT_PASSWORD=admin' -d dpage/pgadmin4
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        private readonly IAuditDataEnricher _auditDataEnricher;

        public UsersDbContext(
            DbContextOptions<AppDbContext> options,
            IAuditDataEnricher auditDataEnricher)
            : base(options) 
        {
            _auditDataEnricher = auditDataEnricher;
        }

        public DbSet<UserEntity> Users { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _auditDataEnricher.Enrich(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Users");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace.Contains("Users"));
            base.OnModelCreating(builder);
        }

        public async Task<IDbContextTransaction> DeleteAllData(CancellationToken cancellationToken)
        {
            var tables = Model.GetEntityTypes().Select(x => $"{x.GetSchema()}.\"{x.GetTableName()}\"");
            var transaction = await Database.BeginTransactionAsync(cancellationToken);
            foreach (var table in tables)
            {
                await Database.ExecuteSqlRawAsync($"TRUNCATE TABLE {table} CASCADE;", cancellationToken: cancellationToken);
            }
            return transaction;
        }
    }
}
