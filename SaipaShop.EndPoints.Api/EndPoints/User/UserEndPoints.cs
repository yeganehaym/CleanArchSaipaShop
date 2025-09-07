using MediatR;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SaipaShop.Application.CQRS.Query;

namespace SaipaShop.EndPoints.Api.EndPoints.User;

public  class UserEndPoints:IEndpoint
{

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app
            .MapGet("/getusers",async (IMediator mediator,[AsParameters]GetUserListQuery query) =>
            {
                var result = await mediator.Send(query);
                return result.MatchEndPoint();
            })
            .WithTags(Tags.Users);


    }
}