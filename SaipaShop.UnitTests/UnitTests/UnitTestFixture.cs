using Microsoft.Extensions.DependencyInjection;

namespace SaipaShop.UnitTests.UnitTests;

public class UnitTestFixture
{
    protected ServiceProvider GetServiceProvider()
    {
        var provider = TestServiceProviderFactory.Create("testdb");
        return provider;
    }
}