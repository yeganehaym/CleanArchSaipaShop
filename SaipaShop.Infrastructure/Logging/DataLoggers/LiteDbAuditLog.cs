using LiteDB;

namespace SaipaShop.Infrastructure.Logging.DataLoggers;

public class LiteDbAuditLog
{
    public ObjectId Id { get; set; } // فقط در زیرساخت دیده میشه
    public string CommandName { get; set; } = default!;
    public BsonDocument? OldData { get; set; }
    public BsonDocument NewData { get; set; } = default!;
    public string? Username { get; set; } = default!;
    public int? UserId { get; set; } = default!;
    public DateTimeOffset ChangedAt { get; set; }
    public string RequestPath { get; set; } = default!;
    public string RequestBody { get; set; } = default!;
}