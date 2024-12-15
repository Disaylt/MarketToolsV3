using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using WB.Seller.Api.Companies.Domain.Constant;
using WB.Seller.Api.Companies.Domain.Seed;
using WB.Seller.Api.Companies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);
ITypingConfigManager<ServiceConfiguration> serviceConfigManager = configurationServiceFactory
    .CreateFromService<ServiceConfiguration>(ServiceInformation.Name);

builder.Services.AddInfrastructureLayer(serviceConfigManager.Value);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
