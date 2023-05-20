using AutoMapper;
using DownNotifier.BackgroundJob.CheckUrl;
using DownNotifier.Business.UrlDefinition;
using DownNotifier.DataAccess.Repository;
using DownNotifier.Entity;
using DownNotifier.Middleware.Exception;
using DownNotifier.Models.Mapping;
using DownNotifier.Notification.Email;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Serilog;
using DownNotifier.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Serilog
builder.Host.UseSerilog((builderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()

    );

builder.Host.AddSerilogConfiguration(); 
#endregion

#region Hangfire
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("HangFire")));
builder.Services.AddHangfireServer();

#endregion

#region DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DownNotifier.Entity.DownNotifierDbContext>(x => x.UseSqlServer(connectionString));

#endregion

#region Mapper
var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(typeof(IMapper), _ => mapper);

#endregion

#region Register Services
builder.Services.AddScoped(typeof(DbContext), typeof(DownNotifierDbContext));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUrlDefinitionService, UrlDefinitionService>();
builder.Services.AddScoped<ICheckUrlService, CheckUrlService>();
builder.Services.AddScoped<IEmailService, EmailService>();

#endregion

#region Identity 
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<DownNotifierDbContext>();

#endregion

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddRazorPages();

var app = builder.Build();

#region MiddleWare
app.UseExceptionMiddleware();

#endregion

app.UseAuthentication();

app.UseHangfireDashboard("/hangdash");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages();
});
Log.Information("Application starting.");
app.Run();


