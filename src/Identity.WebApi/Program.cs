using Asp.Versioning;
using Identity.Application;
using Identity.Domain.Constants;
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
builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfiguration> serviceConfigManager = 
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(IdentityConfig.ServiceName);
serviceConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<AuthConfig> authConfigManager = 
    await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceAuthentication(authConfigManager.Value);
builder.Services.AddWebApiServices();
builder.Services
    .AddMessageBroker(messageBrokerConfigManager.Value)
    .AddInfrastructureLayer(serviceConfigManager.Value)
    .AddApplicationLayer();

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.AddLogging(serviceConfigManager.Value);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AuthDetailsMiddleware>();

app.MapControllers();

app.Run();