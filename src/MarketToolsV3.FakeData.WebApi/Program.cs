using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.FakeData.WebApi;
using MarketToolsV3.FakeData.WebApi.Application;
using MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Extensions;
using MarketToolsV3.FakeData.WebApi.Infrastructure;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

ConfigurationServiceFactory configurationServiceFactory = new(builder.Configuration);

ITypingConfigManager<ServiceConfig> serviceConfigManager =
    await configurationServiceFactory.CreateFromServiceAsync<ServiceConfig>(FakeDataConfig.ServiceName);
serviceConfigManager.AddAsOptions(builder.Services);

builder.AddServiceDefaults();

builder.AddFakeDataLogging();
builder.Services
    .AddInfrastructureService(serviceConfigManager.Value)
    .AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1");
builder.Services.AddSingleton<IJsonValueService, JsonValueService>();

var app = builder.Build();

app.Subscribe<RunFakeDataTaskNotification>()
    .Subscribe<FailFakeDataTasksNotification>()
    .Subscribe<MarkAsHandleNotification>()
    .Subscribe<MarkTaskAsAwaitNotification>()
    .Subscribe<SkipGroupTasksNotification>()
    .Subscribe<ProcessTaskDetailsNotification>()
    .Subscribe<SelectTaskDetailsNotification>()
    .Subscribe<StateTaskDetailsNotification>()
    .Subscribe<TimeoutTaskDetailsNotification>();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

