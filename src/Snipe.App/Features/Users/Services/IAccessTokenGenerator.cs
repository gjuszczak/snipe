namespace Snipe.App.Features.Users.Services
{
    public interface IAccessTokenGenerator
    {
        string Generate(Guid userId, string email);
    }
}
