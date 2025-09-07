using SaipaShop.Application.Common;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;

namespace SaipaShop.EndPoints.Api.EndPoints.Resources;

public class TestResource : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapGet("resource",async (string key, ISharedLocalizer _localizer) =>
            {
                var value = _localizer[key];
                return Results.Ok(value);
            });
    }
}