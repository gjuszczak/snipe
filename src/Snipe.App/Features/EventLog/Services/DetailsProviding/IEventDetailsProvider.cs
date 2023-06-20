using Snipe.App.Core.Events;

namespace Snipe.App.Features.EventLog.Services.DetailsProviding
{
    public interface IEventDetailsProvider
    {
        EventDetails GetDetails(IEvent @event);
    }
}