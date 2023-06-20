using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Core.Dispatchers
{
    public interface IDispatchWrapper<TResult>
    {
        Task<TResult> Dispatch(IServiceProvider serviceProvider, IRequest<TResult> request, CancellationToken cancellationToken);
    }
}