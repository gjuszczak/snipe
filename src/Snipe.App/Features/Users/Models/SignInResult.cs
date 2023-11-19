namespace Snipe.App.Features.Users.Models
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public SignInFailReason? FailReason { get; set; }
        public DateTimeOffset? LockoutExpiration { get; set; }
        public TokenPair? TokenPair { get; set; }

        public SignInResult(bool success, SignInFailReason? failReason, DateTimeOffset? lockoutExpiration, TokenPair? tokenPair)
        {
            Success = success;
            FailReason = failReason;
            LockoutExpiration = lockoutExpiration;
            TokenPair = tokenPair;
        }

        public static SignInResult InvalidEmail()
            => new(false, SignInFailReason.InvalidEmail, null, null);

        public static SignInResult InvalidPassword()
            => new(false, SignInFailReason.InvalidPassword, null, null);

        public static SignInResult UserLockedOut(DateTimeOffset lockoutExpiration)
            => new(false, SignInFailReason.UserLockedOut, lockoutExpiration, null);

        public static SignInResult SignedIn(TokenPair tokenPair)
            => new(true, null, null, tokenPair);
    }
}
