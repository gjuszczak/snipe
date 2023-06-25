namespace Snipe.App.Features.Users.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LockoutExpiration { get; set; }
        
        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}
