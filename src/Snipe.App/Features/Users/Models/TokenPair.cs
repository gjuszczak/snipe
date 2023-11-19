namespace Snipe.App.Features.Users.Models
{
    public class TokenPair
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }

        public TokenPair(string accessToken, string refreshToken)
        {
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
    }
}
