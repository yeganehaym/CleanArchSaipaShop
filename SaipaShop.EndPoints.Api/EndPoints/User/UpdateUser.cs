using SaipaShop.Application.Common;
using SaipaShop.Application.CQRS.Commands;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SaipaShop.EndPoints.Api.EndPoints.User;

public class UpdateUser:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/user/update",
            async ([FromBody] UpdateUserCommand command, IMediator mediator, ISharedLocalizer localizer) =>
            {
                var result = await mediator.Send(command);
                return result.MatchEndPoint(localizer);
            });
    }
}