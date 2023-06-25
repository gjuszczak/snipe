using Snipe.App.Core.Events;
using Snipe.App.Core.Exceptions;
using System.Collections.ObjectModel;

namespace Snipe.App.Core.Aggregates
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly Dictionary<Type, Action<object>> _eventHandlers = new();
        private readonly List<IEvent> _events = new();

        public Guid AggregateId { get; private set; } = Guid.Empty;
        public int Version { get; private set; } = 0;

        private bool IsInitialized => AggregateId != Guid.Empty;

        public IEnumerable<IEvent> GetUncommittedChanges()
        {
            return new ReadOnlyCollection<IEvent>(_events);
        }

        public void MarkChangesAsCommitted()
        {
            Version += _events.Count;
            _events.Clear();
        }

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            var aggregateId = history.FirstOrDefault()?.AggregateId;
            if (!aggregateId.HasValue)
            {
                return;
            }
            if (IsInitialized)
            {
                throw new InvalidOperationException($"Can not load aggregate {GetType().FullName} from history, because it is already initialized. Only fresh Aggregate instances can be loaded from history.");
            }
            AggregateId = aggregateId.Value;
            foreach (IEvent @event in history.OrderBy(e => e.Version))
            {
                if (@event.Version != Version + 1)
                {
                    throw new EventsOutOfOrderException(@event.AggregateId, GetType(), Version + 1, @event.Version);
                }
                if (@event.AggregateId != AggregateId)
                {
                    throw new InvalidOperationException($"Event loaded from history does not belong to Aggregate. AggregateId missmatch (Event.AggregateId='{@event.AggregateId}') (AggregateId='{AggregateId}').");
                }
                Apply(@event);
                Version++;
            }
        }

        protected void RegisterEventHandler<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            _eventHandlers.Add(typeof(TEvent), x => handler((TEvent)x));
        }

        protected void ApplyInitEvent(Guid aggregateId, IEvent @event)
        {
            if (!IsInitialized)
            {
                AggregateId = aggregateId;
            }
            ApplyEvent(@event);
        }

        protected void ApplyEvent(IEvent @event)
        {
            if (!IsInitialized)
            {
                throw new InvalidOperationException($"Cannot Apply {@event.GetType().FullName} event on Aggregate {GetType().FullName}, because it is not initialized yet. Use ApplyInitEvent to initialize aggregate.");
            }
            Apply(@event);
            _events.Add(@event);
        }

        private void Apply(IEvent @event)
        {
            var eventType = @event.GetType();
            if (!_eventHandlers.TryGetValue(eventType, out var handler))
                throw new ArgumentException($"'{eventType.FullName}' is not supported by aggregate {GetType().FullName}", nameof(@event));

            handler(@event);
        }
    }
}
