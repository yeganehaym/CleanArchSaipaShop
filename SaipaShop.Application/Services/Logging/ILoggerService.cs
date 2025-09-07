namespace SaipaShop.Application.Services.Logging;

public interface ILoggerService
{
    Task LogAsync(AuditLog log);

}