using Snipe.App.Features.Users.Models;

namespace Snipe.App.Features.Users.Services
{
    public interface ISignInService
    {
        Task<SignInResult> SignInAsync(string email, string password, CancellationToken cancellationToken);

        Task<SignInWithRefreshTokenResult> SignInWithRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}