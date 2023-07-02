using Snipe.App.Features.Common.Services;
using Snipe.App.Features.Users.Entities;
using System.Text.Json;

namespace Snipe.App.Features.Users.Services
{
    public class ActivityLogGenerator : IActivityLogGenerator
    {
        private readonly IHttpContextDetails _httpContextDetails;

        public ActivityLogGenerator(IHttpContextDetails httpContextDetails)
        {
            _httpContextDetails = httpContextDetails;
        }

        public ActivityLogEntity SignInFailure(Guid userId)
            => CreateActivityLog(userId, ActivityLogKind.SignInFailure, JsonSerializer.SerializeToElement<object?>(null));

        public ActivityLogEntity SignInSuccess(Guid userId)
            => CreateActivityLog(userId, ActivityLogKind.SignInSuccess, JsonSerializer.SerializeToElement<object?>(null));

        public ActivityLogEntity SignInWithRefreshTokenFailure(Guid userId)
            => CreateActivityLog(userId, ActivityLogKind.SignInWithRefreshTokenFailure, JsonSerializer.SerializeToElement<object?>(null));

        public ActivityLogEntity SignInWithRefreshTokenSuccess(Guid userId)
            => CreateActivityLog(userId, ActivityLogKind.SignInWithRefreshTokenSuccess, JsonSerializer.SerializeToElement<object?>(null));

        private ActivityLogEntity CreateActivityLog(Guid userId, ActivityLogKind kind, JsonElement details)
            => new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Details = details,
                Kind = kind,
                CreatedAt = DateTime.UtcNow,
                CreatedByIp = _httpContextDetails.GetRemoteIp() ?? string.Empty,
                CreatedByUserAgent = _httpContextDetails.GetUserAgent() ?? string.Empty,
            };

    }
}
