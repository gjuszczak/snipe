using Snipe.App.Core.Commands;
using Snipe.App.Features.Common.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Features.EventLog.Commands.ReplayEvents
{
    public class ReplayEvents : Command
    {
        public string Name { get; set; }
    }

    public class ReplayEventsHandler : ICommandHandler<ReplayEvents>
    {
        private readonly IEventsReplayService _eventsReplayService;

        public ReplayEventsHandler(IEventsReplayService eventsReplayService)
        {
            _eventsReplayService = eventsReplayService;
        }

        public async Task<Guid> Handle(ReplayEvents command, CancellationToken cancellationToken)
        {
            await _eventsReplayService.ReplayAsync(cancellationToken);
            return command.CorrelationId;
        }
    }
}
