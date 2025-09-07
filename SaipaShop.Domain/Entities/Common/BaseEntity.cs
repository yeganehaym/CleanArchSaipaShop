using SaipaShop.Domain.DomainEvents;

namespace SaipaShop.Domain.Entities.Common;

public class BaseEntity:DomainEntity,IBaseEntity,IBaseRemovedEntity
{
    public int Id { get; set; }
    public bool IsRemoved { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset? ModificationDate { get; set; }
}