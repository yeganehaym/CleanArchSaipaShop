using SaipaShop.Domain.Entities.Users;

namespace SaipaShop.Domain.Repositories;

public interface IUserRepository
{
    Task UpdateUserLastActivityDateAsync(int userId);
    ValueTask<User> FindUserAsync(int userId);
    Task<User> LoginWithPersonAsync(string username, string password);
    
    Task<bool> UserExistsAsync(string username);

    int? GetCurrentUserId();

    Task<User> GetCurrentUserAsync();
    
    Task CreateUserAsync(User user);

}