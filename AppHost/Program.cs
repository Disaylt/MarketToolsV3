using Projects;

var builder = DistributedApplication.CreateBuilder(args);

IReadOnlyCollection<IResourceBuilder<ProjectResource>> projects =
[
    builder.AddProject<Identity_WebApi>("IdentityWebApiService"),
    builder.AddProject<UserNotifications_Processor>("UserNotificationProcessor"),
    builder.AddProject<UserNotifications_WebApi>("UserNotificationWebApi"),
    builder.AddProject<WB_Seller_Companies_WebApi>("WbSellerCompaniesWebapi"),
    builder.AddProject<MarketToolsV3_ApiGateway>("MarketToolsV3ApiGateway"),
    builder.AddProject<Identity_GrpcService>("IdentityGrpcService"),
    builder.AddProject<WB_Seller_Companies_Processor>("WbSellerCompaniesProcessor"),
    builder.AddProject<MarketToolsV3_FakeData_WebApi>("MarketToolsV3FakeDataWebApi"),
    builder.AddProject<Ozon_Seller_Companies_WebApi>("OzonSellerCompaniesWebApi"),
    builder.AddProject<MarketToolsV3_PermissionStore_GrpcService>("MarketToolsV3PermissionStoreGrpcService")
];

var configTypeParameter = builder.AddParameter("ConfigType");
var jsonBasePathParameter = builder.AddParameter("JsonBasePath");

foreach (var project in projects)
{
    project.WithEnvironment("ConfigType", configTypeParameter)
        .WithEnvironment("JsonBasePath", jsonBasePathParameter)
        .WithEndpoint("https", endpoint => endpoint.IsProxied = false, false)
        .WithEndpoint("http", endpoint => endpoint.IsProxied = false, false);
}

builder.AddProject<Projects.WB_Seller_Features_Automation_PriceManager_Processor>("wb-seller-features-automation-pricemanager-processor");

builder.Build().Run();
