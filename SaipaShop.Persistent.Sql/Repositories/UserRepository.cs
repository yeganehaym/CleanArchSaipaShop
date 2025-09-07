using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SaipaShop.Application.Common;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Repositories;
using SaipaShop.Persistent.Sql.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace SaipaShop.Persistent.Sql.Repositories;

public class UserRepository:IUserRepository
{
    private DbSet<User> _users;
    private IHttpContextAccessor _httpContextAccessor;
    
    public UserRepository(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _users = uow.Set<User>();
    }

    public async Task UpdateUserLastActivityDateAsync(int userId)
    {
        var user = await _users.FirstOrDefaultAsync(x => x.Id == userId);
        if(user==null)
            return;
        user.LastLoggedIn=DateTimeOffset.Now;
    }

    public async ValueTask<User> FindUserAsync(int userId)
    {
        return await _users.FirstOrDefaultAsync(x => x.Id == userId);
    }
    
    public async Task<User> LoginWithPersonAsync(string username, string password)
    {
        return await _users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _users.AnyAsync(x=>x.Username == username);
    }

    public int? GetCurrentUserId()
    {
        var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var userDataClaim = claimsIdentity?.FindFirst(JwtRegisteredClaimNames.NameId);
        var userId = userDataClaim?.Value;

        if (userId == null)
            return null;

        return int.Parse(userId);
    }

    public async Task<User> GetCurrentUserAsync()
    {
        var userId=GetCurrentUserId();
        if (userId == null)
            return null;
        
        var user=await _users.FirstOrDefaultAsync(x => x.Id == userId);

        return user;
    }

    public async Task CreateUserAsync(User user)
    {
        await _users.AddAsync(user);
    }
}