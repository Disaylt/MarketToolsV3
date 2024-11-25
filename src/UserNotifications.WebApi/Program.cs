using Asp.Versioning;
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

builder.Services.AddApplicationLayer(serviceSection)
    .AddInfrastructureServices(serviceSection);

builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);
});

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
