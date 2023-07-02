namespace Snipe.App.Features.Users.Services
{
    public class EmailVerificationTotpProvider : IEmailVerificationTotpProvider
    {
        public static readonly Guid EmailVerificationTokenSeed = new("34834652-3bcc-4750-9b9b-38a2b08fcc21");

        private readonly ITotpProvider _totpProvider;

        public EmailVerificationTotpProvider(ITotpProvider totpProvider)
        {
            _totpProvider = totpProvider;
        }

        public string GenerateCode(string email)
            => _totpProvider.GenerateCode(EmailVerificationTokenSeed, GetModifier(email));

        public bool ValidateCode(string email, string code)
            => _totpProvider.ValidateCode(EmailVerificationTokenSeed, GetModifier(email), code);

        private static string GetModifier(string email)
            => $"EmailVerification:{email}";
    }
}
