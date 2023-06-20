using Snipe.App.Features.Redirections.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Snipe.App.Features.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<RedirectionEntity> Redirections { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<IDbContextTransaction> DeleteAllData(CancellationToken cancellationToken);
    }
}
