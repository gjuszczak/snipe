namespace Snipe.App.Features.Users.Models
{
    public class SignInWithRefreshTokenResult
    {
        public bool Success { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
