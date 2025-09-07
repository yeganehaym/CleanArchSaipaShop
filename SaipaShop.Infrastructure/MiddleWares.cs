using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace SaipaShop.Infrastructure;

public static class MiddleWares{

    public static WebApplication UseAppHangFire(this WebApplication app)
    { 
        app.UseHangfireDashboard();

        return app;
    }
    
    
    public static WebApplication UseAppSerilogConfig(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            // Customize the message template
            options.MessageTemplate = "Handled {RequestPath}";
    
            // Emit debug-level events instead of the defaults
            options.GetLevel = (httpContext, elapsed, ex) => app.Environment.IsDevelopment()? LogEventLevel.Warning : LogEventLevel.Warning;
    
            
        });

        return app;
    }
}

