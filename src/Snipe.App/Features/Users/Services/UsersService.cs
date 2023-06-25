using Microsoft.EntityFrameworkCore;
using Snipe.App.Features.Users.Entities;
using Snipe.App.Features.Users.Models;

namespace Snipe.App.Features.Users.Services
{
    public class UsersService
    {
        private readonly IUsersDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthTokenGenerator _tokenGenerator;

        public UsersService(IUsersDbContext context, IPasswordHasher passwordHasher, IAuthTokenGenerator tokenGenerator)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<SignInResult> SignIn(string email, string password, CancellationToken cancellationToken)
        {
            var userEntity = await _context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

            var isLockedOut = userEntity?.LockoutExpiration != null
                    && userEntity.LockoutExpiration >= DateTime.Now;

            var isPasswordCorrect = userEntity != null
                && _passwordHasher.Verify(password, userEntity.PasswordHash);

            if (userEntity != null && !isLockedOut && isPasswordCorrect)
            {
                return await SignInSuccess(userEntity, cancellationToken);
            }

            return await SignInFail(userEntity, isLockedOut, isPasswordCorrect, cancellationToken);
        }

        private async Task<SignInResult> SignInSuccess(UserEntity userEntity, CancellationToken cancellationToken)
        {
            var accessToken = _tokenGenerator.GenerateAccessToken(userEntity.Id, userEntity.Email);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();

            return new SignInResult
            {
                Success = true,
                UserId = userEntity.Id,
            };
        }


        private async Task<SignInResult> SignInFail(UserEntity userEntity, bool isLockedOut, bool isPasswordCorrect, CancellationToken cancellationToken)
        {
            if (userEntity == null)
            {
                return new SignInResult
                {
                    Success = false,
                    FailReason = SignInFailReason.InvalidEmail
                };
            }

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
    }
}
