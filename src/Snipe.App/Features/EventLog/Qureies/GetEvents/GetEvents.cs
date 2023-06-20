﻿using Snipe.App.Core.Events;
using Snipe.App.Core.Queries;
using Snipe.App.Features.Common.Models;
using Snipe.App.Features.EventLog.Services.DetailsProviding;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Features.EventLog.Qureies.GetEvents
{
    public class GetEvents : PaginatedQuery, IQuery<EventsListDto>
    {
        public Guid? AggregateId { get; set; }
    }

    public class GetEventsHandler : IQueryHandler<GetEvents, EventsListDto>
    {
        private readonly IEventStore _eventStore;
        private readonly IEventDetailsProvider _eventDetailsProvider;

        public GetEventsHandler(IEventStore eventStore, IEventDetailsProvider eventDetailsProvider)
        {
            _eventStore = eventStore;
            _eventDetailsProvider = eventDetailsProvider;
        }

        public async Task<EventsListDto> Handle(GetEvents request, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAsync(request.AggregateId, request.First, request.Rows, cancellationToken);
            var totalCount = await _eventStore.CountAsync(request.AggregateId, cancellationToken);
            var dtos = events.Select(EventToDto).ToList();

            return new EventsListDto(
                dtos,
                totalCount,
                request.First,
                request.Rows,
                request.AggregateId);
        }

        private EventDto EventToDto(IEvent @event)
        {
            var details = _eventDetailsProvider.GetDetails(@event);
            return EventDto.FromEvent(@event, details);
        }
    }
}
