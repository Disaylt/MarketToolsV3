using Identity.Application;
using Identity.Domain.Seed;
using Identity.Infrastructure;
using Identity.WebApi;
using Identity.WebApi.Services;
using MarketToolsV3.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.LoadConfigurationAsync();

IConfigurationSection serviceSection = builder.Configuration.GetSection("Identity");
builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(serviceSection);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebApiServices(serviceSection);

builder.Services
    .AddInfrastructureLayer(serviceSection)
    .AddApplicationLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();