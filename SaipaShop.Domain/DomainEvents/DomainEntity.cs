using SaipaShop.Domain.Entities.Common;

namespace SaipaShop.Domain.DomainEvents;

public class DomainEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public ICollection<IDomainEvent> GetDomainEvents() => _domainEvents;

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}