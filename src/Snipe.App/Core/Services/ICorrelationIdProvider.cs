namespace Snipe.App.Core.Services
{
    public interface ICorrelationIdProvider
    {
        Guid GetCorrelationIdIfEmpty(Guid existingCorelationId);
        Guid GetCorrelationId();
    }
}
