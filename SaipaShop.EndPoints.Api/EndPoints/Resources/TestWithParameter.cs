using SaipaShop.Application.Common;
using SaipaShop.Domain.Errors;
using SaipaShop.Domain.ResultPattern;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;

namespace SaipaShop.EndPoints.Api.EndPoints.Resources;

public class TestWithParameter:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/TestWithParameter", (ISharedLocalizer sharedLocalizer) =>
        {
            var result = Result<bool>.Failure(new TestWithParamsError(new[] { "Username", "34" }));
            
            return result.MatchEndPoint(sharedLocalizer);
        });
    }
}