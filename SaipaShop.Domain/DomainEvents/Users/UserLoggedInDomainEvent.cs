using SaipaShop.Domain.Entities.Users;

namespace SaipaShop.Domain.DomainEvents.Users;

public class UserLoggedInDomainEvent:IDomainEvent
{
    public User User { get; set; }
    public DateTime LoggedInTime { get; set; }
}