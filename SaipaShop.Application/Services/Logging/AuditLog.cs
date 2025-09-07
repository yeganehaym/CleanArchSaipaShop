namespace SaipaShop.Application.Services.Logging;

public class AuditLog
{
        public object? OldData { get; set; }
        public object? NewData { get; set; }
        public string? Username { get; set; } = default!;
        public int? UserId { get; set; } = default!;
        public DateTimeOffset ChangedAt { get; set; }
        public string RequestPath { get; set; } = default!;
        public string RequestBody { get; set; } = default!;
        public string CommandName { get; set; }
}
