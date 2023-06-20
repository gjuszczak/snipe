using Microsoft.EntityFrameworkCore;

namespace Snipe.Infrastructure.Persistence
{
    public interface IAuditDataEnricher
    {
        void Enrich(DbContext context);
    }
}