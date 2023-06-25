namespace Snipe.App.Features.Common.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        Task<string> GetAccessTokenAsync();
    }
}
