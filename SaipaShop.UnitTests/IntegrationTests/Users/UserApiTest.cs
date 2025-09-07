using System.Text;
using System.Text.Json;
using SaipaShop.Shared.Constants;

namespace SaipaShop.UnitTests.IntegrationTests.Users;

public class UserApiTest:TestClassFixture
{
    [Fact]
    public async Task LoginUsers()
    {
        // Arrange
        var loginData = new
        {
            username = "admin",
            password = "123456"
        };

        var jsonContent = new StringContent(
            JsonSerializer.Serialize(loginData),
            Encoding.UTF8,
            "application/json");

        // Act

        var url = GetUrl(EndPointsAddress.Login);
        var response = await _client.PostAsync(url,jsonContent);
        
        
        //Assert
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("token", json);
    }
}