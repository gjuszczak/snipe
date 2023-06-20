using Snipe.App.Features.Redirections.Queries;
using System.Threading.Tasks;

namespace Snipe.Infrastructure.Services.Redirections
{
    public interface ICachedRedirectionsService
    {
        Task<RedirectionDto> GetByNameAsync(string name);
    }
}