using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Snipe.App.Features.Users.Services
{
    public class AuthTokenGenerator : IAuthTokenGenerator
    {
        private readonly IAuthTokenConfig _authTokenConfig;

        public AuthTokenGenerator(IAuthTokenConfig authTokenConfig)
        {
            _authTokenConfig = authTokenConfig;
        }

        public string GenerateAccessToken(Guid userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authTokenConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Email, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_authTokenConfig.AccessTokenTtl),
                Issuer = _authTokenConfig.Issuer,
                Audience = _authTokenConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64).Concat(Guid.NewGuid().ToByteArray()).ToArray();
            var randomString = Convert.ToBase64String(randomBytes);
            return randomString;
        }
    }
}
