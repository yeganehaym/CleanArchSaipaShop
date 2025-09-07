using SaipaShop.Application.Services.Logging;
using LiteDB;

namespace SaipaShop.Infrastructure.Logging.DataLoggers;

public class LightDbLoggerService:ILoggerService
{
    public async Task LogAsync(AuditLog entry)
    {
        var log = new LiteDbAuditLog
        {
            OldData = entry.OldData != null ? BsonMapper.Global.ToDocument(entry.OldData) : null,
            NewData = BsonMapper.Global.ToDocument(entry.NewData),
            UserId = entry.UserId,
            Username = entry.Username,
            CommandName = entry.CommandName,
            ChangedAt = entry.ChangedAt,
            RequestPath = entry.RequestPath,
            RequestBody = entry.RequestBody
        };

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audit", "audit.db");
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using var db = new LiteDatabase(path);
        var col = db.GetCollection<LiteDbAuditLog>("logs");
        col.Insert(log);
    }

}