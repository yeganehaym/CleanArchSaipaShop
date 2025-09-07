using System.Security.Claims;

namespace SaipaShop.Infrastructure.Authentication.Jwt;

public class JwtTokensData
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string RefreshTokenSerial { get; set; }
    public IEnumerable<Claim> Claims { get; set; }
}