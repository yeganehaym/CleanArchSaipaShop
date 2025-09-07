using Microsoft.AspNetCore.Http.HttpResults;
using SaipaShop.Domain.Entities;
using SaipaShop.Domain.Primitives;
using SaipaShop.EndPoints.Api.EndPoints.Configurations;

namespace SaipaShop.EndPoints.Api.EndPoints;

public class PersonEndPoint:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/personnel/{code}", (string code) =>
        {
            var person = new Person();
            person.PersonnelCode = PersonnelCode.Create(code);
            return Results.Ok(new { code = person.PersonnelCode });
        });
    }
}