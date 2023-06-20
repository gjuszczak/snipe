﻿using System.Threading;
using System.Threading.Tasks;

namespace Snipe.App.Core.Dispatchers
{
    public interface IHandler<in TRequest, TResult> 
        where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest command, CancellationToken cancellationToken);
    }
}
