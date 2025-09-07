using SaipaShop.Domain.Enums;

namespace SaipaShop.Application.Dto.Users;

public class UserDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string MobileNo { get; set; }
    
    
    public UserStatus Status { get; set; }
    public string SerialNumber { get; set; }
    public DateTimeOffset? LastLoggedIn { get; set; }
}