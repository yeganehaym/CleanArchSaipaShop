using ElmahCore;
using Microsoft.AspNetCore.Http;

namespace SaipaShop.Infrastructure.Logging.Elmah;

public class ElmahNotFoundErrorFilter:IErrorFilter
{
    public void OnErrorModuleFiltering(object sender, ExceptionFilterEventArgs args)
    {
        if (args.Exception.GetBaseException() is FileNotFoundException)
            args.Dismiss();
        if (args.Context is HttpContext httpContext)
            if (httpContext.Response.StatusCode == 404)
                args.Dismiss();
    }
}