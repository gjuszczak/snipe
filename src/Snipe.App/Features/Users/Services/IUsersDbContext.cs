using Snipe.App.Features.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Snipe.App.Features.Users.Services
{
    public interface IUsersDbContext
    {
        DbSet<UserEntity> Users { get; }
        DbSet<RefreshTokenEntity> RefreshTokens { get; }
        DbSet<ActivityLogEntity> ActivityLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
