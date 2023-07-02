using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Snipe.App.Features.Users.Entities;
using Snipe.App.Features.Users.Services;

namespace Snipe.Infrastructure.Persistence.Users
{
    // Add-Migration Initial -c UsersDbContext -o Persistence/Users/Migrations
    // docker run --name postgresdb -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres:15-alpine
    // docker run --name pgadmin4 -p 8080:80 -e 'PGADMIN_DEFAULT_EMAIL=admin@admin.com' -e 'PGADMIN_DEFAULT_PASSWORD=admin' -d dpage/pgadmin4
    public class UsersDbContext : DbContext, IUsersDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<ActivityLogEntity> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("users");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), x => x.Namespace.Contains("Users"));
            base.OnModelCreating(builder);
        }
    }
}
