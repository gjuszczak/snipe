namespace Snipe.App.Features.Users.Services
{
    public interface IEmailVerificationTotpProvider
    {
        string GenerateCode(string email);
        bool ValidateCode(string email, string code);
    }
}