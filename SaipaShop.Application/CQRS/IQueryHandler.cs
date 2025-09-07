using SaipaShop.Domain.ResultPattern;
using MediatR;

namespace SaipaShop.Application.CQRS;

public interface IQueryHandler<Tin,Tout> : IRequestHandler<Tin,Result<Tout>> where Tin:IRequest<Result<Tout>>
{
    
}