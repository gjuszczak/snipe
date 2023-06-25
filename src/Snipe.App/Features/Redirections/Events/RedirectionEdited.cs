using Snipe.App.Core.Events;
using Snipe.App.Features.Common.Services;

namespace Snipe.App.Features.Redirections.Events
{
    public class RedirectionEdited : RedirectionBaseEvent
    {
        public RedirectionEdited(string name, Uri url)
            : base(name, url) { }
    }

    public class RedirectionEditedHandler : IEventHandler<RedirectionEdited>
    {
        private readonly IAppDbContext _appDbContext;

        public RedirectionEditedHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(RedirectionEdited @event, CancellationToken cancellationToken)
        {
            var entity = await _appDbContext.Redirections.FindAsync(new object[] { @event.AggregateId }, cancellationToken);
            entity.Name = @event.Name;
            entity.Url = @event.Url.ToString();
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
