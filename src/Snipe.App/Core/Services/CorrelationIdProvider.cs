namespace Snipe.App.Core.Services
{
    public class CorrelationIdProvider : ICorrelationIdProvider
    {
        private readonly Guid _correlationId = Guid.NewGuid();

        public Guid GetCorrelationId()
        {
            return _correlationId;
        }

        public Guid GetCorrelationIdIfEmpty(Guid existingCorelationId)
        {
            if (existingCorelationId == Guid.Empty)
                return _correlationId;
            return existingCorelationId;
        }
    }
}
