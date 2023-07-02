namespace Snipe.App.Features.Users.Entities
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
        public string CreatedByUserAgent { get; set; }

        public UserEntity User { get; set; } = default!;
        public RefreshTokenRevokeDetailsEntity? RevokeDetails { get; set; }
    }
}
