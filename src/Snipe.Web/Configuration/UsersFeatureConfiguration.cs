using Snipe.App.Features.Users.Services;

namespace Snipe.Web.Configuration
{
    public class UsersFeatureConfiguration : IUsersFeatureConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int AccessTokenTtl { get; set; }
        public int RefreshTokenTtl { get; set; }
    }
}
