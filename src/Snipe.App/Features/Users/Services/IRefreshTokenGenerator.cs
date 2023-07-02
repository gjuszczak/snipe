using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public interface IRefreshTokenGenerator
    {
        RefreshTokenEntity Generate(Guid userId);
    }
}