using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public class UserLockoutService : IUserLockoutService
    {
        public bool IsLockedOut(UserEntity userEntity)
            => userEntity.LockoutExpiration != null && userEntity.LockoutExpiration >= DateTimeOffset.Now;

        public DateTimeOffset? GetLockoutExpiration(UserEntity userEntity)
            => IsLockedOut(userEntity) ? userEntity.LockoutExpiration : null;

        public void ResetLockout(UserEntity userEntity)
        {
            userEntity.AccessFailedCount = 0;
            userEntity.LockoutExpiration = null;
        }

        public void IncrementFailCount(UserEntity userEntity)
        {
            if (IsLockedOut(userEntity))
            {
                return;
            }

            userEntity.AccessFailedCount++;
            
            if (userEntity.AccessFailedCount >= 5)
            {
                userEntity.LockoutExpiration = DateTimeOffset.UtcNow.AddMinutes(5);
            }
        }
    }
}
