using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MiraeDigital.BffMobile.API.Accessors;
using MiraeDigital.BffMobile.API.Extensions;
using MiraeDigital.BffMobile.Domain.SeedWork;
using MiraeDigital.Lib.WebApi.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostContext, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(hostContext.Configuration);
});

builder.Services.AddHttpContextAccessor();
builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<IRequestAccessor, HttpRequestAccessor>();
builder.Services.AddScoped<IUserAccessor, HttpUserAccessor>();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddRefitClient(builder.Configuration);
builder.Services.AddHttpClientExtention(builder.Configuration);
builder.Services.AddMiraeWebApi(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddMiraePresenterWebApi();
builder.Services.AddMemoryCache();


var app = builder.Build();
app.UseMiddleware<IPCaptureMiddleware>();
app.UseMiraeWebApi(app.Configuration);

app.Run();

public partial class Program { }