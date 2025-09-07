using SaipaShop.Application.Dto.Users;
using SaipaShop.Application.RepositoryFilters;
using SaipaShop.Domain.DomainDto.Pagination;
using SaipaShop.Domain.Entities.Common;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;
using MapsterMapper;
using SaipaShop.Application.Dto;
using SaipaShop.Domain.Errors;

namespace SaipaShop.Application.CQRS.Query;

public class GetUserListQuery:UserListFilter, IQuery<PagedResult<UserDto>>
{
    
}

public class GetUserListQueryHandler:IQueryHandler<GetUserListQuery,PagedResult<UserDto>>
{
    private IGenericRepository<User> _repository;
    private IMapper _mapper;

    public GetUserListQueryHandler(IGenericRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<Result<PagedResult<UserDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var users = await _repository.GetAllWithFiltersInDtoAsync<User,SimpleUserDto>(request, cancellationToken);

        if (users.Results.Count() == 0)
        {
            return Result<PagedResult<UserDto>>.Failure(DomainErrors.Errors.NoResultFound);
        }
        
        var dto=_mapper.Map<PagedResult<UserDto>>(users);

        return Result<PagedResult<UserDto>>.Success(dto);
    }
}