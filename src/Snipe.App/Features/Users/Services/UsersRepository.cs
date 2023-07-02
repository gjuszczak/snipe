using Microsoft.EntityFrameworkCore;
using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IUsersDbContext _context;

        public UsersRepository(IUsersDbContext context)
        {
            _context = context;
        }

        public void AddUser(UserEntity user)
            => _context.Users.Add(user);

        public void AddRefreshToken(RefreshTokenEntity refreshToken)
            => _context.RefreshTokens.Add(refreshToken);

        public void AddActivityLog(ActivityLogEntity activityLog)
            => _context.ActivityLogs.Add(activityLog);

        public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

        public async Task<UserEntity?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var dbRefreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .SingleOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);
            return dbRefreshToken?.User;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
