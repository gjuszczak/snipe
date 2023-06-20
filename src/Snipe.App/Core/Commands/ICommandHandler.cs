using Snipe.App.Core.Dispatchers;
using System;

namespace Snipe.App.Core.Commands
{
    public interface ICommandHandler<in TCommand> : IHandler<TCommand, Guid> 
        where TCommand : ICommand
    {
    }
}
