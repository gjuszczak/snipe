using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Features.Common.Interfaces
{
    public interface IEventsReplayService
    {
        Task ReplayAsync(CancellationToken cancellationToken);
    }
}