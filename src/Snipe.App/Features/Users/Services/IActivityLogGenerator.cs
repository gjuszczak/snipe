using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public interface IActivityLogGenerator
    {
        ActivityLogEntity SignInFailure(Guid userId);
        ActivityLogEntity SignInSuccess(Guid userId);
        ActivityLogEntity SignInWithRefreshTokenFailure(Guid userId);
        ActivityLogEntity SignInWithRefreshTokenSuccess(Guid userId);
    }
}