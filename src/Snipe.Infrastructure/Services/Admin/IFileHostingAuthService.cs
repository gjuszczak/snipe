using System.Threading;
using System.Threading.Tasks;

namespace Snipe.Infrastructure.Services.Admin
{
    public interface IFileHostingAuthService
    {
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
    }
}