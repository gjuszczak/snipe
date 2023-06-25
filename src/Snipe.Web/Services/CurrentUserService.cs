using Snipe.App.Features.Common.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Snipe.Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor?.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string UserId => _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<string> GetAccessTokenAsync() => await _httpContext.GetTokenAsync("access_token");
    }
}
