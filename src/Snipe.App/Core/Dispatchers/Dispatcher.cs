﻿using Snipe.App.Core.Commands;
using Snipe.App.Core.Queries;
using Snipe.App.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Core.Dispatchers
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICorrelationIdProvider _correlationIdProvider;
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, object> _dispatchWrappers = new();

        public Dispatcher(ICorrelationIdProvider correlationIdProvider, IServiceProvider serviceProvider)
        {
            _correlationIdProvider = correlationIdProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<Guid> DispatchAsync(ICommand command, CancellationToken cancellationToken = default)
        {
            _correlationIdProvider.SetCorrelationId(command.CorrelationId);
            var commandType = command.GetType();
            var commandDispatchWrapper = (IDispatchWrapper<Guid>)_dispatchWrappers.GetOrAdd(commandType,
                commandType => CreateDispatchWrapper<Guid>(
                    commandType,
                    typeof(ICommandHandler<>).MakeGenericType(commandType),
                    _serviceProvider));
            return await commandDispatchWrapper.Dispatch(_serviceProvider, command, cancellationToken);
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            var queryType = query.GetType();
            var queryDispatchWrapper = (IDispatchWrapper<TResult>)_dispatchWrappers.GetOrAdd(queryType,
                queryType => CreateDispatchWrapper<TResult>(
                    queryType,
                    typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult)),
                    _serviceProvider));
            return await queryDispatchWrapper.Dispatch(_serviceProvider, query, cancellationToken);
        }

        private static IDispatchWrapper<TResult> CreateDispatchWrapper<TResult>(Type requestType, Type handlerType, IServiceProvider serviceProvider)
        {
            var dispatchHelperType = typeof(DispatchWrapper<,,>).MakeGenericType(requestType, typeof(TResult), handlerType);
            var pipelineProvider = serviceProvider.GetRequiredService<IPipelineProvider>();
            return (IDispatchWrapper<TResult>)Activator.CreateInstance(dispatchHelperType, pipelineProvider);
        }
    }
}
