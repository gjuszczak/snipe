namespace Snipe.App.Features.Common.Services
{
    public interface IHttpContextDetails
    {
        string? GetRemoteIp();
        string? GetUserAgent();
    }
}
