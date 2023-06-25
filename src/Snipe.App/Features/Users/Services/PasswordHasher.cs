using BC = BCrypt.Net.BCrypt;

namespace Snipe.App.Features.Users.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool Verify(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
