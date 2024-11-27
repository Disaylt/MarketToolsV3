using Asp.Versioning;
using Identity.Application;
using Identity.Domain.Seed;
using Identity.Infrastructure;
using Identity.WebApi;
using Identity.WebApi.Middlewares;
using Identity.WebApi.Services;
using MarketToolsV3.ConfigurationManager;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.LoadConfigurationAsync();

IConfigurationSection serviceSection = builder.Configuration.GetSection("Identity");
builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(serviceSection);

builder.Services.ConfigureOptions<ServiceConfiguration>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebApiServices(serviceSection);

builder.Services
    .AddInfrastructureLayer(serviceSection)
    .AddApplicationLayer();

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.AddLogging(serviceSection);

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