using Snipe.App.Features.Users.Entities;
using Snipe.App.Features.Users.Models;

namespace Snipe.App.Features.Users.Services
{
    public class SignInService : ISignInService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IActivityLogGenerator _activityLogGenerator;

        public SignInService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IAccessTokenGenerator accessTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IActivityLogGenerator activityLogGenerator)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _activityLogGenerator = activityLogGenerator;
        }

        public async Task<SignInResult> SignInAsync(string email, string password, CancellationToken cancellationToken)
        {
            var userEntity = await _usersRepository.GetByEmailAsync(email, cancellationToken);

            var isLockedOut = userEntity?.LockoutExpiration != null
                    && userEntity.LockoutExpiration >= DateTime.Now;

            var isPasswordCorrect = userEntity != null
                && _passwordHasher.Verify(password, userEntity.PasswordHash);

            if (userEntity != null && !isLockedOut && isPasswordCorrect)
            {
                return await SignInSuccess(userEntity, cancellationToken);
            }

            return await SignInFailure(userEntity, isLockedOut, cancellationToken);
        }

        private async Task<SignInResult> SignInSuccess(UserEntity userEntity, CancellationToken cancellationToken)
        {
            var accessToken = _accessTokenGenerator.Generate(userEntity.Id, userEntity.Email);

            var refreshToken = _refreshTokenGenerator.Generate(userEntity.Id);
            _usersRepository.AddRefreshToken(refreshToken);

            var activityLog = _activityLogGenerator.SignInSuccess(userEntity.Id);
            _usersRepository.AddActivityLog(activityLog);

            await _usersRepository.SaveChangesAsync(cancellationToken);

            return new SignInResult
            {
                Success = true,
                UserId = userEntity.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<SignInResult> SignInFailure(UserEntity? userEntity, bool isLockedOut, CancellationToken cancellationToken)
        {
            if (userEntity == null)
            {
                return new SignInResult
                {
                    Success = false,
                    FailReason = SignInFailReason.InvalidEmail
                };
            }

            var activityLog = _activityLogGenerator.SignInFailure(userEntity.Id);
            _usersRepository.AddActivityLog(activityLog);

            await _usersRepository.SaveChangesAsync(cancellationToken);

            if (isLockedOut)
            {
                return new SignInResult
                {
                    Success = false,
                    FailReason = SignInFailReason.UserLockedOut,
                    LockoutExpiration = userEntity.LockoutExpiration,
                    UserId = userEntity.Id
                };
            }

            return new SignInResult
            {
                Success = false,
                FailReason = SignInFailReason.InvalidPassword,
                UserId = userEntity.Id
            };
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
