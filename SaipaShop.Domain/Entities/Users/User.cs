using SaipaShop.Domain.DomainEvents.Users;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Enums;

namespace SaipaShop.Domain.Entities.Users;

public class User:BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string MobileNo { get; set; }
    
    
    public UserStatus Status { get; set; }
    public string SerialNumber { get; set; }
    public DateTimeOffset? LastLoggedIn { get; set; }

    public void Login()
    {
        Raise(new UserLoggedInDomainEvent());
    }
}