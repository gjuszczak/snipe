namespace Snipe.App.Features.Users.Models
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public Guid? UserId { get; set; }
        public SignInFailReason? FailReason { get; set; }
        public DateTime? LockoutExpiration { get; internal set; }
    }
}
