using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.UnitTests.UnitTests.Users;

public class UserServiceTest:UnitTestFixture
{
    [Fact]
    public async Task IsUserExists_WhenUserExists_ReturnsTrue()
    {
        // Arrange
        var provider = GetServiceProvider();
        var userRepository = provider.GetRequiredService<IUserRepository>();


        // Act
        var result=await userRepository.UserExistsAsync("test");

        // Assert

        Assert.True(result);
    }
}