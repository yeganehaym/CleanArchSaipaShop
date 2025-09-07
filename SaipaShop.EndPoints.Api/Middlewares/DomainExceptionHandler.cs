using SaipaShop.Application.Common;
using SaipaShop.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace SaipaShop.EndPoints.Api.Middlewares;

internal sealed class DomainExceptionHandler : IExceptionHandler
{
    private IServiceScopeFactory _serviceScopeFactory;

    public DomainExceptionHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        using (var scope = _serviceScopeFactory.CreateScope())
        {


            if (exception is not DomainException domainException)
            {
                return false;
            }


            var _sharedLocalizer = scope.ServiceProvider.GetRequiredService<ISharedLocalizer>();
   
            var value1 = _sharedLocalizer[domainException.Message];
            var value2 = _sharedLocalizer[domainException.Description];

            var problemDetails = new
            {
                Status = StatusCodes.Status417ExpectationFailed,
                Title = "Bad Request",
                
                Detail = $"{value1} {value2}"
            };
            
            

            httpContext.Response.StatusCode = problemDetails.Status;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}