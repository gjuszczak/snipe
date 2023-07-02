using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public interface IUsersRepository
    {
        void AddActivityLog(ActivityLogEntity activityLog);
        void AddRefreshToken(RefreshTokenEntity refreshToken);
        void AddUser(UserEntity user);
        Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}