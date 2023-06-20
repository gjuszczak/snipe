using Snipe.App.Core.Aggregates;

namespace Snipe.App.Features.EventLog.Services.DetailsProviding
{
    public interface IAggregateDetailsProvider
    {
        AggregateDetails GetDetails(IAggregateRoot value);
    }
}
