using SaipaShop.Application.Common;
using SaipaShop.Application.Dto.Users;
using SaipaShop.Domain.Errors;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;
using MapsterMapper;

namespace SaipaShop.Application.CQRS.Commands;

public class UpdateUserCommand : UserDto, ICommand<UserDto>
{
}

public class UpdateUserCommandHandler:ICommandHandler<UpdateUserCommand,UserDto>
{
    private IUnitOfWork _uow;
    private IUserRepository _userRepository;
    private IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork uow, IUserRepository userRepository, IMapper mapper)
    {
        _uow = uow;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user =await _userRepository.FindUserAsync(request.Id);
        
        if(user == null)
            return Result<UserDto>.Failure(DomainErrors.Errors.NoResultFound);

        _mapper.Map(request, user);
        
        var rows=await _uow.SaveAllChangesAsync(cancellationToken);
        
        if (rows > 0)
        {
            var dto=_mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(dto);
        }

        return null;
    }
}