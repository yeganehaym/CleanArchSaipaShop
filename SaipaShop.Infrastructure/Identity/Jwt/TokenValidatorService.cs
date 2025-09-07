using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SaipaShop.Domain.Enums;
using SaipaShop.Domain.Repositories;
using SaipaShop.Infrastructure.Authentication.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SaipaShop.Infrastructure.Identity.Jwt
{
    public interface ITokenValidatorService
    {
        Task ValidateAsync(TokenValidatedContext context);
    }

    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IUserRepository _usersRepository;
        private readonly ITokenStoreService _tokenStoreService;

        public TokenValidatorService(IUserRepository usersRepository, ITokenStoreService tokenStoreService)
        {
            _usersRepository = usersRepository;

            _tokenStoreService = tokenStoreService;
        }

        public async Task ValidateAsync(TokenValidatedContext context)
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            if (claimsIdentity?.Claims == null || !claimsIdentity.Claims.Any())
            {
                context.Fail("This is not our issued token. It has no claims.");
                return;
            }

            var serialNumberClaim = claimsIdentity.FindFirst(ClaimTypes.SerialNumber);
            if (serialNumberClaim == null)
            {
                context.Fail("This is not our issued token. It has no serial.");
                return;
            }

            var userIdString = claimsIdentity.FindFirst(ClaimTypes.UserData).Value;
            if (!int.TryParse(userIdString, out int userId))
            {
                context.Fail("This is not our issued token. It has no user-id.");
                return;
            }

            var user = await _usersRepository.FindUserAsync(userId);
            if (user == null || user.SerialNumber != serialNumberClaim.Value || user.Status != UserStatus.Accepted)
            {
                // user has changed his/her password/roles/stat/IsActive
                context.Fail("This token is expired. Please login again.");
            }

            if (!(context.SecurityToken is JwtSecurityToken accessToken) || string.IsNullOrWhiteSpace(accessToken.RawData) ||
                !await _tokenStoreService.IsValidTokenAsync(accessToken.RawData, userId))
            {
                context.Fail("This token is not in our database.");
                return;
            }

            await _usersRepository.UpdateUserLastActivityDateAsync(userId);
        }
    }
}
