using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public interface IUserLockoutService
    {
        DateTimeOffset? GetLockoutExpiration(UserEntity userEntity);
        void IncrementFailCount(UserEntity userEntity);
        bool IsLockedOut(UserEntity userEntity);
        void ResetLockout(UserEntity userEntity);
    }
}
