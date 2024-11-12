using MarketToolsV3.ConfigurationManager;
using UserNotifications.Applications;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
await builder.Configuration.LoadConfigurationAsync();

IConfigurationSection serviceSection = builder.Configuration.GetSection("UserNotifications");
builder.Services.AddOptions<ServiceConfiguration>()
    .Bind(serviceSection);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationLayer()
    .AddInfrastructureServices(serviceSection);

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
