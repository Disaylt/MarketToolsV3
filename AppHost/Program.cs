using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var configTypeParameter = builder.AddParameter("ConfigType");
var jsonBasePathParameter = builder.AddParameter("JsonBasePath");

builder
    .AddProject<MarketToolsV3_MigrationService>("MigrationService")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder
    .AddProject<Identity_WebApi>("IdentityWebApi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder
    .AddProject<UserNotifications_Processor>("UserNotificationProcessor")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder
    .AddProject<WB_Seller_Api_Companies_WebApi>("WbSellerApiCompaniesWebApi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.Build().Run();
