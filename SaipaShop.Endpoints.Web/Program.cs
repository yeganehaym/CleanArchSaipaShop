using SaipaShop.Application;
using SaipaShop.Infrastructure;
using SaipaShop.Persistent.Sql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
             

// Add services to the container.

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration,builder.Environment)
    .AddPersistent(builder.Configuration,builder.Environment.IsEnvironment("Testing"))
    .AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();