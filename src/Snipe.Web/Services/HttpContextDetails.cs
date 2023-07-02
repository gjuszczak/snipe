using Snipe.App.Features.Common.Services;

namespace Snipe.Web.Services
{
    public class HttpContextDetails : IHttpContextDetails
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextDetails(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetRemoteIp()
            => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        public string? GetUserAgent()
            => _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString();
    }
}
