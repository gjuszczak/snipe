using Snipe.App.Core.Dispatchers;
using Snipe.App.Features.EventLog.Commands.ReplayEvents;
using Snipe.App.Features.EventLog.Qureies.GetAggregate;
using Snipe.App.Features.EventLog.Qureies.GetEvents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Snipe.Web.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/event-log")]
    public class EventLogController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public EventLogController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("events")]
        public async Task<EventsListDto> GetEvents([FromQuery] GetEvents query, CancellationToken cancellationToken)
        {
            return await _dispatcher.DispatchAsync(query, cancellationToken);
        }

        [HttpGet("aggregates/{aggregateId}")]
        public async Task<AggregateDto> GetAggregate([FromRoute] GetAggregate query, CancellationToken cancellationToken)
        {
            return await _dispatcher.DispatchAsync(query, cancellationToken);
        }

        [HttpPost("replay-events")]
        public async Task ReplayEvents(ReplayEvents command, CancellationToken cancellationToken)
        {
            await _dispatcher.DispatchAsync(command, cancellationToken);
        }
    }
}
