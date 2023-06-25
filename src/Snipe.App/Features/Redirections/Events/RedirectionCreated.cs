using Snipe.App.Core.Events;
using Snipe.App.Features.Common.Services;
using Snipe.App.Features.Redirections.Entities;

namespace Snipe.App.Features.Redirections.Events
{
    public class RedirectionCreated : RedirectionBaseEvent
    {
        public RedirectionCreated(string name, Uri url)
            : base(name, url) { }
    }

    public class RedirectionCreatedHandler : IEventHandler<RedirectionCreated>
    {
        private readonly IAppDbContext _appDbContext;

        public RedirectionCreatedHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(RedirectionCreated @event, CancellationToken cancellationToken)
        {
            _appDbContext.Redirections.Add(new RedirectionEntity
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                Url = @event.Url.ToString(),
            });
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
