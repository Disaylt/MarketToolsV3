using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolV3.Authentication;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WB.Seller.Companies.Application;
using WB.Seller.Companies.Domain.Constants;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure;
using WB.Seller.Companies.WebApi.Utilities.Implementation;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory
        .CreateFromServiceAsync<ServiceConfiguration>(ServiceConstants.ServiceName)
        .ContinueWith(task =>
        {
            task.Result.AddAsOptions(builder.Services);

            return task.Result;
        });

ITypingConfigManager<AuthConfig> authConfigManager =
    await configurationServiceFactory.CreateFromAuthAsync();

ITypingConfigManager<ModulesInfoConfig> modulesInfoConfigManager =
    await configurationServiceFactory.CreateFromModulesInfoAsync()
        .ContinueWith(task =>
        {
            task.Result.AddAsOptions(builder.Services);

            return task.Result;
        });

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services
    .AddInfrastructureLayer(serviceConfigManager.Value)
    .AddApplicationServices()
    .AddServiceAuthentication(authConfigManager.Value, false);

builder.Services.AddOpenApi("v1", opt =>
{
    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

app.MapDefaultEndpoints();

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