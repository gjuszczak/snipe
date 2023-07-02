namespace Snipe.App.Features.Users.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }

        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutExpiration { get; set; }
        public int AccessFailedCount { get; set; }

        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();

        public override string ToString()
            => Email ?? string.Empty;
    }
}
