namespace Snipe.App.Features.Users.Services
{
    public interface ITotpProvider
    {
        string GenerateCode(Guid tokenSeed, string modifier);
        bool ValidateCode(Guid tokenSeed, string modifier, string code);
    }
}