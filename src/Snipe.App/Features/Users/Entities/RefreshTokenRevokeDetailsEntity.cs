namespace Snipe.App.Features.Users.Entities
{
    public class RefreshTokenRevokeDetailsEntity
    {
        public DateTimeOffset RevokedAt { get; set; }
        public string RevokedByIp { get; set; }
        public string RevokedByUserAgent { get; set; }
    }
}
