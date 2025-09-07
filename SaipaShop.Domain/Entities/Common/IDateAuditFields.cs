namespace SaipaShop.Domain.Entities.Common;

public interface IDateAuditFields
{
    DateTimeOffset CreationDate { get; set; }
    DateTimeOffset? ModificationDate { get; set; } 
}