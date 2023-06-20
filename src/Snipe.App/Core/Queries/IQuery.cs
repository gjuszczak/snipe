using Snipe.App.Core.Dispatchers;

namespace Snipe.App.Core.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
