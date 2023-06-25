namespace Snipe.App.Features.Users.Services
{
    public interface IAuthTokenGenerator
    {
        string GenerateAccessToken(Guid userId, string email);
        string GenerateRefreshToken();
    }
}
