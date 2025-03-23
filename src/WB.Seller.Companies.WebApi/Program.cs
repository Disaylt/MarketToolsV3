using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using WB.Seller.Companies.Application;
using WB.Seller.Companies.Domain.Constants;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure;
using WB.Seller.Companies.WebApi;

var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServiceConfiguration> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfiguration>(ServiceConstants.ServiceName);
serviceConfigManager.AddAsOptions(builder.Services);
ITypingConfigManager<AuthConfig> authConfigManager =
    await configurationServiceFactory.CreateFromAuthAsync();

builder.AddServiceDefaults();

builder.Services.AddControllers();

builder.Services
    .AddInfrastructureLayer(serviceConfigManager.Value)
    .AddApplicationServices()
    .AddServiceAuthentication(authConfigManager.Value);

builder.Services.AddOpenApi("v1");

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

app.MapControllers();

app.Run();
