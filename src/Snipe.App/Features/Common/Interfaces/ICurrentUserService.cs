using System.Threading.Tasks;

namespace Snipe.App.Features.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        Task<string> GetAccessTokenAsync();
    }
}
