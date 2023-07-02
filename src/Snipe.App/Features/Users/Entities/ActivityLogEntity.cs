using System.Text.Json;

namespace Snipe.App.Features.Users.Entities
{
    public class ActivityLogEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ActivityLogKind Kind { get; set; }
        public JsonElement Details { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByIp { get; set; }
        public string CreatedByUserAgent { get; set; }
    }
}
