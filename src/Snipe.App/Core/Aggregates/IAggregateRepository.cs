using Snipe.App.Core.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Core.Aggregates
{
    public interface IAggregateRepository
    {
        Task SaveAsync<TAggregateRoot>(TAggregateRoot aggregate, int? expectedVersion = null, CancellationToken cancellationToken = default)
            where TAggregateRoot : IAggregateRoot, new();

        Task<TAggregateRoot> GetAsync<TAggregateRoot>(Guid aggregateId, IList<IEvent> events = null, CancellationToken cancellationToken = default)
            where TAggregateRoot : IAggregateRoot, new();
    }
}
