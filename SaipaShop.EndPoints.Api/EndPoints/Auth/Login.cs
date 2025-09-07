using SaipaShop.Application.Dto.Users;
using SaipaShop.Domain.Enums;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;
using SaipaShop.Infrastructure.Authentication.Jwt;
using SaipaShop.Infrastructure.Identity.Jwt;
using SaipaShop.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using SaipaShop.Domain.Primitives;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace SaipaShop.EndPoints.Api.EndPoints.Auth;

public class Login:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(EndPointsAddress.Login, async ([FromBody]LoginDto dto, ITokenFactoryService tokenFactoryService) =>
            {

                var personnelCode=PersonnelCode.Create("343242");
                
                if (dto.Username == "admin" && dto.Password == "123456")
                {
                    var user = new Domain.Entities.Users.User()
                    {
                        Username = dto.Username,
                        Password = dto.Password,
                        Email = "yeganehaym@gmail.com",
                        FirstName = "ali",
                        LastName = "yeganeh",
                        SerialNumber = "123",
                        Status = UserStatus.Accepted
                    };
                    var token = await tokenFactoryService.CreateJwtTokensAsync(user);
                    return Results.Ok(new { token = token.AccessToken });
                }

                return  Results.NoContent();
            })
            .WithTags(Tags.Users)
            .WithOpenApi(cfg =>
            {
                cfg.Summary = "Login By Username and password";
                return cfg;
            });
    }
}