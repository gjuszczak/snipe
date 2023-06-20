using Snipe.Infrastructure.Persistence.App;
using Snipe.Infrastructure.Persistence.Events;
using Microsoft.EntityFrameworkCore;

namespace Snipe.Web.Services
{
    public class DbMigrationsBootstrap : IHostedService
    {
        private readonly IServiceProvider _provider;

        public DbMigrationsBootstrap(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();

            var eventsDbContext = scope.ServiceProvider.GetService<EventsDbContext>();
            await eventsDbContext.Database.MigrateAsync(cancellationToken);

            var appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
            await appDbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
