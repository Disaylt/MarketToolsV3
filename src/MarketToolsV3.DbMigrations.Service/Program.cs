using MarketToolsV3.DbMigrations.Service.Extensions;
using MarketToolsV3.DbMigrations.Service.Services.Implementation;
using MarketToolsV3.DbMigrations.Service.Services.Interfaces;
using MarketToolsV3.DbMigrations.Service;

var builder = Host.CreateApplicationBuilder(args);

await builder.AddIdentityMigration();

builder.Services.AddSingleton<IWorkNotificationServiceService, WorkNotificationServiceService>();
builder.Services.AddSingleton<IWorkControlService, WorkControlService>();

builder.AddServiceDefaults();

builder.DetermineTotalMigrationServices();
var host = builder.Build();

host.Services.GetRequiredService<IWorkControlService>();


host.Run();
