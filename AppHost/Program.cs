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
    .AddProject<UserNotifications_WebApi>("UserNotificationWebApi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<WB_Seller_Companies_WebApi>("WbSellerCompaniesWebapi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<MarketToolsV3_ApiGateway>("MarketToolsV3ApiGateway")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<Identity_GrpcService>("IdentityGrpcService")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<WB_Seller_Companies_Processor>("WbSellerCompaniesProcessor")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.AddProject<MarketToolsV3_FakeData_WebApi>("MarketToolsV3FakeDataWebApi")
    .WithEnvironment("ConfigType", configTypeParameter)
    .WithEnvironment("JsonBasePath", jsonBasePathParameter);

builder.Build().Run();
