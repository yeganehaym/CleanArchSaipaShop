using MediatR;
using Microsoft.AspNetCore.Mvc;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.EndPoints.Api.UserEndPoints;

public static class UsersEndPoints
{
    public static void MapUsers(this WebApplication app)
    {
        app.MapGet("/saipa/{id}", (int id, IMediator mediator) =>
        {
            
        });

        app.MapPost("/saipa", ([FromBody] User user) =>
        {

        });
    }
}