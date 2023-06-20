using System.Threading;
using System.Threading.Tasks;

namespace Snipe.Infrastructure.Services.Admin
{
    public interface IBackupFileService
    {
        Task<TemporaryPath> GenerateAsync(CancellationToken cancellationToken);
        Task RestoreAsync(string filePath, CancellationToken cancellationToken);
    }
}
