using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.DbMigrations.Service.Attributes;
using MarketToolsV3.DbMigrations.Service.Extensions;
using MarketToolsV3.DbMigrations.Service.Models;
using MarketToolsV3.DbMigrations.Service.Services.Implementation;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using MarketToolsV3.DbMigrations.Service.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IWorkNotificationServiceService, WorkNotificationServiceService>();
builder.Services.AddSingleton<IWorkControlService, WorkControlService>();

builder.AddServiceDefaults();

builder.DetermineTotalMigrationServices();
var host = builder.Build();

host.Services.GetRequiredService<IWorkControlService>();


host.Run();
