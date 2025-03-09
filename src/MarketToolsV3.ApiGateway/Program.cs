using MarketToolsV3.ApiGateway;
using MarketToolsV3.ApiGateway.Domain.Constants;
using MarketToolsV3.ApiGateway.Middlewares;
using MarketToolsV3.ApiGateway.Services.Implementation;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Proto.Contract.Identity;
using Scalar.AspNetCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthContext, AuthContext>();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

IConfigManager serviceConfigManager = await configurationServiceFactory.CreateFromServiceAsync(ApiGatewayConfig.ServiceName);
serviceConfigManager.JoinTo(builder.Configuration);

var authConfigManager = await configurationServiceFactory.CreateFromAuthAsync();
authConfigManager.AddAsOptions(builder.Services);

ITypingConfigManager<ServicesAddressesConfig> servicesAddressesConfigManager 
    = await configurationServiceFactory.CreateFromServicesAddressesAsync();

builder.Services
    .AddAuthGrpcClient(servicesAddressesConfigManager.Value)
    .AddApiGatewayServices();

builder.Services
    .AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

string corsName = builder.Services
    .AddDevCorsServices();

var app = builder.Build();


app.UseCors(corsName);

app.UseMiddleware<AuthContextMiddleware>();
app.UseMiddleware<AccessTokenMiddleware>();
app.UseMiddleware<TokensRefreshMiddleware>();
app.UseMiddleware<SessionMiddleware>();
app.UseMiddleware<HeadersTokensAdapterMiddleware>();

await app.UseOcelot();

app.UseMiddleware<CookiesInjectMiddleware>();

app.Run();
