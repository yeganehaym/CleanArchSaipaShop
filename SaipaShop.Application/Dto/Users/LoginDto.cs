using System.ComponentModel.DataAnnotations;

namespace SaipaShop.Application.Dto.Users;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    
}