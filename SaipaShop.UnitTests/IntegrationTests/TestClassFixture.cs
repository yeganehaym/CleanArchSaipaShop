namespace SaipaShop.UnitTests.IntegrationTests;

public class TestClassFixture:IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient _client;

    public TestClassFixture()
    {
        var factory = new CustomWebApplicationFactory();
        _client = factory.CreateClient();
    }

    protected string GetUrl(string endpointAddress)
    {
        return "/api/v1" + endpointAddress;
    }
}