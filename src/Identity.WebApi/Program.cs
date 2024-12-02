using Asp.Versioning;
using Identity.Application;
using Identity.Domain.Seed;
using Identity.Infrastructure;
using Identity.WebApi;
using Identity.WebApi.Middlewares;
using Identity.WebApi.Services;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);
string serviceName = "Identity";

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory.CreateFromService<ServiceConfiguration>(serviceName);
serviceConfigManager.AddAsOptions(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddWebApiServices(globalConfig);

//builder.Services
//    .AddInfrastructureLayer(globalConfig)
//    .AddApplicationLayer();

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

//builder.AddLogging(globalConfig);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CookieTokenAdapterMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthDetailsMiddleware>();

app.MapControllers();

app.Run();