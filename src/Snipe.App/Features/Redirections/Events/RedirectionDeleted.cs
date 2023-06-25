using Snipe.App.Core.Events;
using Snipe.App.Features.Common.Services;

namespace Snipe.App.Features.Redirections.Events
{
    public class RedirectionDeleted : Event
    {
    }

    public class RedirectionDeletedHandler : IEventHandler<RedirectionDeleted>
    {
        private readonly IAppDbContext _appDbContext;

        public RedirectionDeletedHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(RedirectionDeleted @event, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Redirections.FindAsync(new object[] { @event.AggregateId }, cancellationToken);
            _appDbContext.Redirections.Remove(entity);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
