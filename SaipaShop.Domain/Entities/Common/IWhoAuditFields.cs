namespace SaipaShop.Domain.Entities.Common;

public interface IWhoAuditFields
{
    int? WhoCreated { get; set; }
    int? WhoDidLastModification { get; set; } 
}