namespace Snipe.App.Features.Users.Services
{
    public interface IAuthTokenConfig
    {
        public string Issuer { get; }
        public string Audience { get; }
        public string Secret { get; }
        public int AccessTokenTtl { get; }
        public int RefreshTokenTtl { get; }
    }
}
