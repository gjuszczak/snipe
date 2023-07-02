namespace Snipe.App.Features.Users.Services
{
    public class EmailVerificationSender : IEmailVerificationSender
    {
        private readonly IEmailVerificationTotpProvider _emailVerificationTotpProvider;

        public EmailVerificationSender(IEmailVerificationTotpProvider emailVerificationTotpProvider)
        {
            _emailVerificationTotpProvider = emailVerificationTotpProvider;
        }

        public string SendEmailVerificationCode(string email)
        {
            var code = _emailVerificationTotpProvider.GenerateCode(email);
            return code;
        }
    }
}
