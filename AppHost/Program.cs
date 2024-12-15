using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var configTypeParameter = builder.AddParameter("ConfigType");
var jsonBasePathParameter = builder.AddParameter("JsonBasePath");

builder
    .AddProject<Identity_WebApi>("IdentityWebApiService")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder
    .AddProject<UserNotifications_Processor>("UserNotificationProcessor")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder
    .AddProject<WB_Seller_Api_Companies_WebApi>("wb-seller-api-companies-webapi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<Projects.MarketToolsV3_MigrationService>("markettoolsv3-migrationservice");

builder.Build().Run();
