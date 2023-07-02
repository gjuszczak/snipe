namespace Snipe.App.Features.Users.Services
{
    public interface IUsersService
    {
        Task CreateUserAsync(string email, string password, string emailVerificationCode, CancellationToken cancellationToken);
    }
}