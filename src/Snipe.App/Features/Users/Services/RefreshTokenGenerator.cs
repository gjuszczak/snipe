using Snipe.App.Features.Common.Services;
using Snipe.App.Features.Users.Entities;
using System.Security.Cryptography;

namespace Snipe.App.Features.Users.Services
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly IHttpContextDetails _httpContextDetails;

        public RefreshTokenGenerator(IHttpContextDetails httpContextDetails)
        {
            _httpContextDetails = httpContextDetails;
        }

        public RefreshTokenEntity Generate(Guid userId)
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64).Concat(Guid.NewGuid().ToByteArray()).ToArray();
            var randomString = Convert.ToBase64String(randomBytes);
            var remoteIp = _httpContextDetails.GetRemoteIp() ?? string.Empty;
            var remoteUserAgent = _httpContextDetails.GetUserAgent() ?? string.Empty;
            return new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = randomString,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = remoteIp,
                CreatedByUserAgent = remoteUserAgent
            };
        }
    }
}
