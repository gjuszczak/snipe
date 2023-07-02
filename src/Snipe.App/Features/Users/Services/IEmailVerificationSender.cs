namespace Snipe.App.Features.Users.Services
{
    public interface IEmailVerificationSender
    {
        string SendEmailVerificationCode(string email);
    }
}