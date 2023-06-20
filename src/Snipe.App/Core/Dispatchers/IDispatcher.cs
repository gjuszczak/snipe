using Snipe.App.Core.Commands;
using Snipe.App.Core.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Core.Dispatchers
{
    public interface IDispatcher
    {
        Task<Guid> DispatchAsync(ICommand command, CancellationToken cancellationToken = default);
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
    }
}