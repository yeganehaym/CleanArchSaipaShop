using SaipaShop.Application.Services.Security;
using SaipaShop.Domain.Entities.Users;
using SaipaShop.Domain.Enums;
using SaipaShop.Domain.Errors;
using SaipaShop.Domain.Repositories;
using SaipaShop.Domain.ResultPattern;

namespace SaipaShop.Application.CQRS.Query;


public class LoginQuery:IQuery<User>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginQueryHandler : IQueryHandler<LoginQuery, User>
{
    private IUserRepository _repository;
    private IHashService _hashService;
    
    public LoginQueryHandler(IUserRepository repository, IHashService hashService)
    {
        _repository = repository;
        _hashService = hashService;
    }

    public async Task<Result<User>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var hashedPassword = _hashService.GetSha256Hash(request.Password);
        var user =await _repository.LoginWithPersonAsync(request.Username, hashedPassword);
        
        if(user==null)
            return Result<User>.Failure(DomainErrors.Errors.UserNotFoundError);
        
        if (user.Status != UserStatus.Accepted)
            return Result<User>.Failure(DomainErrors.Errors.UserNotAccepted);

        user.Login();
        return Result<User>.Success(user);
    }
}