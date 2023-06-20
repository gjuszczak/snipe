using Snipe.App.Core.Dispatchers;

namespace Snipe.App.Core.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
    }
}
