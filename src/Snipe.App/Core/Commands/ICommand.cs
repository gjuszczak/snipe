using Snipe.App.Core.Dispatchers;
using System;

namespace Snipe.App.Core.Commands
{
    public interface ICommand : IRequest<Guid>
	{
		Guid Id { get; }
		Guid CorrelationId { get; }
		int ExpectedVersion { get; }
	}
}
