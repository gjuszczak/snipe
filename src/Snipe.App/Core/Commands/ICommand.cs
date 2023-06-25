using Snipe.App.Core.Dispatchers;

namespace Snipe.App.Core.Commands
{
    public interface ICommand : IRequest<Guid>
	{
		Guid Id { get; }
		int ExpectedVersion { get; }

		Guid CorrelationId { get; set; }
	}
}
