using SaipaShop.Application.Common;
using SaipaShop.Application.Dto.Users;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;
using MapsterMapper;

namespace SaipaShop.Application.CQRS.Commands;

public class CreateUserCommand:UserDto,ICommand<UserDto>
{
    
}

public class CreateUserCommandvHandler:ICommandHandler<CreateUserCommand,UserDto>
{
    private IUnitOfWork _uow;
    private IUserRepository _userRepository;
    private IMapper _mapper;
    public CreateUserCommandvHandler(IUnitOfWork uow, IUserRepository userRepository, IMapper mapper)
    {
        _uow = uow;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user=_mapper.Map<User>(request);
        await _userRepository.CreateUserAsync(user);
        var rows = await _uow.SaveAllChangesAsync(cancellationToken);

        if (rows > 0)
        {
            var dto=_mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(dto);
        }

        return null;
    }
}