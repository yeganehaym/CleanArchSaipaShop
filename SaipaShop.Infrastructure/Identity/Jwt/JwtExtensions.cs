using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SaipaShop.Infrastructure.Identity.Jwt;

public static class JwtExtensions
{
    public static int? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var claimsIdentity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var userDataClaim = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.NameId);
        var userId = userDataClaim?.Value;

        if (userId == null)
            return null;

        return int.Parse(userId);
    }
}