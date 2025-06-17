using Asp.Versioning;
using Identity.Application;
using Identity.Domain.Seed;
using Identity.Infrastructure;
using Identity.WebApi;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Extensions;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolV3.Authentication;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHttpContextAccessor();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
addressesConfig.AddAsOptions(builder.Services);

var module = addressesConfig.Value.GetIdentityModule();

ITypingConfigManager<ServiceConfiguration> serviceConfigManager = 
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(module.Name);
serviceConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<AuthConfig> authConfigManager = 
    await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<MessageBrokerConfig> messageBrokerConfigManager =
    await configurationServiceFactory.CreateFromMessageBrokerAsync();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi("v1");
builder.Services.AddServiceAuthentication(authConfigManager.Value, false);
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

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthContext();

app.MapControllers();

app.Run();