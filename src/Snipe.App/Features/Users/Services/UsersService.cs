using Snipe.App.Features.Users.Entities;

namespace Snipe.App.Features.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailVerificationTotpProvider _emailVerificationTotpProvider;

        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IEmailVerificationTotpProvider emailVerificationTotpProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _emailVerificationTotpProvider = emailVerificationTotpProvider;
        }

        public async Task CreateUserAsync(string email, string password, string emailVerificationCode, CancellationToken cancellationToken)
        {
            var isEmailVerified = _emailVerificationTotpProvider.ValidateCode(email, emailVerificationCode);
            if (!isEmailVerified)
            {
                return;
            }

            var userAlreadyExists = (await _usersRepository.GetByEmailAsync(email, cancellationToken)) != null;
            if (userAlreadyExists)
            {
                return;
            }

            var passwordHash = _passwordHasher.Hash(password);
            _usersRepository.AddUser(new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                PasswordHash = passwordHash,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = true,
                LockoutExpiration = null
            });

            await _usersRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
