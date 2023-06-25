namespace Snipe.App.Features.Common.Services
{
    public interface IEventsReplayService
    {
        Task ReplayAsync(CancellationToken cancellationToken);
    }
}