using System;

namespace Snipe.App.Core.Services
{
    public interface ICorrelationIdProvider
    {
        void SetCorrelationId(Guid correlationId);
        Guid GetCorrelationId();
    }
}
