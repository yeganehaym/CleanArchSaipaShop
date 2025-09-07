using System.Text;
using SaipaShop.Application.Services.Logging;
using SaipaShop.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SaipaShop.Application.CQRS.Behaviours;

/// <summary>
/// سیستم لاگ audit
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class AuditBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILoggerService _loggerService;
    private IUserRepository _userRepository;
    IHttpContextAccessor  _httpContextAccessor;

    public AuditBehavior(ILoggerService loggerService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _loggerService = loggerService;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        TResponse response = await next();

        string requestName = request.ToString();

        //فقط باید مباحث ذخیره سازی لاگ شود و کوئری نیازی ندارد
        // چون همه کامندها با نام Command پایان میباند فقط کامندها را لاگ میکنیم
        if (requestName.EndsWith("Command"))
        {
            Type requestType = request.GetType();
            string commandName = requestType.Name;

            var user = await _userRepository.GetCurrentUserAsync();
            var log = new AuditLog()
            {
                CommandName=commandName,
                ChangedAt = DateTimeOffset.Now,
                OldData = request,
                NewData = response,
                UserId = user?.Id,
                Username = user?.Username,
                RequestPath = _httpContextAccessor.HttpContext?.Request.Path.Value ?? "",
                RequestBody = await GetRequestBodyAsync(),
                
            };
            
            await _loggerService.LogAsync(log);

        }

        return response;
    }

    private async Task<object?> LoadCurrentStateAsync(string entityName, string entityId)
    {
        // این متد باید از دیتابیس مقدار فعلی شیء با entityName و entityId رو بارگذاری کنه.
        // اینجا فقط نمونه هست، شما باید منطق واقعی خودتو بذاری.
        return null;
    }

    private async Task<string> GetRequestBodyAsync()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null || !request.Body.CanSeek)
            return "";

        request.Body.Seek(0, System.IO.SeekOrigin.Begin);
        using var reader = new System.IO.StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Seek(0, System.IO.SeekOrigin.Begin);
        return body;
    }
}