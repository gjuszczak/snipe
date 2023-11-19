using Snipe.App.Features.Users.Entities;
using Snipe.App.Features.Users.Models;

namespace Snipe.App.Features.Users.Services
{
    public class SignInService : ISignInService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserLockoutService _userLockoutService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IActivityLogGenerator _activityLogGenerator;

        public SignInService(
            IUsersRepository usersRepository,
            IUserLockoutService userLockoutService,
            IPasswordHasher passwordHasher,
            IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IActivityLogGenerator activityLogGenerator)
        {
            _usersRepository = usersRepository;
            _userLockoutService = userLockoutService;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _activityLogGenerator = activityLogGenerator;
        }

        public async Task<SignInResult> SignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetByEmailAsync(email, cancellationToken);

            var isLockedOut = userEntity != null
                && _userLockoutService.IsLockedOut(userEntity);

            var isPasswordCorrect = userEntity != null
                && _passwordHasher.Verify(password, userEntity.PasswordHash);

            if (userEntity != null && !isLockedOut && isPasswordCorrect)
            {
                return await SignInSuccess(userEntity, cancellationToken);
            }

            return await SignInFailure(userEntity, cancellationToken);
        }

        private async Task<SignInResult> SignInSuccess(UserEntity userEntity, CancellationToken cancellationToken)
        {
            var accessToken = _accessTokenGenerator.Generate(userEntity.Id, userEntity.Email);

            var refreshToken = _refreshTokenGenerator.Generate(userEntity.Id);
            _usersRepository.AddRefreshToken(refreshToken);

            var activityLog = _activityLogGenerator.SignInSuccess(userEntity.Id);
            _usersRepository.AddActivityLog(activityLog);

            await _usersRepository.SaveChangesAsync(cancellationToken);

            return SignInResult.SignedIn(new(accessToken, refreshToken.Token));
        }

        private async Task<SignInResult> SignInFailure(UserEntity? userEntity, CancellationToken cancellationToken)
        {
            if (userEntity == null)
            {
                return SignInResult.InvalidEmail();
            }

            var activityLog = _activityLogGenerator.SignInFailure(userEntity.Id);
            _usersRepository.AddActivityLog(activityLog);

            _userLockoutService.IncrementFailCount(userEntity);

            await _usersRepository.SaveChangesAsync(cancellationToken);

            var lockoutExpiration = _userLockoutService.GetLockoutExpiration(userEntity);
            if (lockoutExpiration.HasValue)
            {
                return SignInResult.UserLockedOut(lockoutExpiration.Value);
            }

            return SignInResult.InvalidPassword();
        }

        public async Task<SignInWithRefreshTokenResult> SignInWithRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetByRefreshTokenAsync(refreshToken, cancellationToken);
            if (userEntity != null)
            {
                var accessToken = _accessTokenGenerator.Generate(userEntity.Id, userEntity.Email);

                var newRefreshToken = _refreshTokenGenerator.Generate(userEntity.Id);
                _usersRepository.AddRefreshToken(newRefreshToken);

                var activityLogSuccess = _activityLogGenerator.SignInWithRefreshTokenSuccess(userEntity.Id);
                _usersRepository.AddActivityLog(activityLogSuccess);

                await _usersRepository.SaveChangesAsync(cancellationToken);

                return new SignInWithRefreshTokenResult
                {
                    Success = true,
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken.Token,
                };
            }

            var activityLogFailure = _activityLogGenerator.SignInWithRefreshTokenFailure(userEntity.Id);
            _usersRepository.AddActivityLog(activityLogFailure);

            await _usersRepository.SaveChangesAsync(cancellationToken);

            return new SignInWithRefreshTokenResult { Success = false };
        }
    }
}
