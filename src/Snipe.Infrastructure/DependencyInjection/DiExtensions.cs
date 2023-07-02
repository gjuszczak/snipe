using Snipe.App.Core.Aggregates;
using Snipe.App.Core.Commands;
using Snipe.App.Core.Dispatchers;
using Snipe.App.Core.Events;
using Snipe.App.Core.Queries;
using Snipe.App.Core.Services;
using Snipe.App.Features.Common.Services;
using Snipe.App.Features.Backups.Services;
using Snipe.App.Features.EventLog.Services.DetailsProviding;
using Snipe.App.Features.Users.Services;
using Snipe.Infrastructure.Persistence;
using Snipe.Infrastructure.Persistence.App;
using Snipe.Infrastructure.Persistence.Events;
using Snipe.Infrastructure.Persistence.Users;
using Snipe.Infrastructure.Services.Admin;
using Snipe.Infrastructure.Services.Common;
using Snipe.Infrastructure.Services.Redirections;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Snipe.Infrastructure
{
    public static class DiExtensions
    {
        public static void AddSnipe(this IServiceCollection services, IConfiguration config)
        {
            services.AddMemoryCache();

            services.AddSingleton<IEventEntityBuilder, EventEntityBuilder>();
            services.AddScoped<IAggregateContext, AggregateContext>();
            services.AddScoped<IAggregateRepository, AggregateRepository>();
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddScoped<IPipelineProvider, PipelineProvider>();
            services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();

            services.AddDbContext<EventsDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("Snipe"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "events")));
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("Snipe"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "app")));
            services.AddDbContext<UsersDbContext>(options =>
                options.UseNpgsql(
                    config.GetConnectionString("Snipe"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "users")));

            services.AddScoped<IAuditDataEnricher, AuditDataEnricher>();
            services.AddScoped<IEventStorage, SqlEventStorage>();
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
            services.AddScoped<IUsersDbContext>(provider => provider.GetService<UsersDbContext>());

            services.Scan(selector =>
            {
                selector.FromAssemblyOf<ICommand>()
                    .AddClasses(filter =>
                    {
                        filter.AssignableToAny(
                            typeof(ICommandHandler<>),
                            typeof(IQueryHandler<,>),
                            typeof(IEventHandler<>),
                            typeof(IPipelineBehaviour<,>));
                    })
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddValidatorsFromAssemblyContaining<ICommand>();

            services.AddSingleton<IEventDetailsProvider, EventDetailsProvider>();
            services.AddSingleton<IAggregateDetailsProvider, AggregateDetailsProvider>();
            services.AddSingleton(SensitiveDataMaskConfiguration.Create()
                .Build());

            services.AddScoped<IEventsReplayService, EventsReplayService>();
            services.AddScoped<IBackupService, BackupService>();
            services.AddScoped<IBackupFileService, BackupFileService>();
            services.AddScoped<ICachedRedirectionsService, CachedRedirectionsService>();

            services.AddSingleton<IAccessTokenGenerator, AccessTokenGenerator>();
            services.AddSingleton<IEmailVerificationSender, EmailVerificationSender>();
            services.AddSingleton<IEmailVerificationTotpProvider, EmailVerificationTotpProvider>();
            services.AddSingleton<ITotpProvider, TotpProvider>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IActivityLogGenerator, ActivityLogGenerator>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddHttpClient<IFileHostingService, FileHostingService>(c =>
            {
                c.BaseAddress = new Uri(config.GetValue<string>("FileHosting:ApiUrl"));
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.Configure<FileHostingAuthConfiguration>(config.GetSection("FileHosting:Auth"));
            services.AddHttpClient<IFileHostingAuthService, FileHostingAuthService>(c =>
            {
                c.BaseAddress = new Uri(config.GetValue<string>("FileHosting:Auth:Instance"));
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
