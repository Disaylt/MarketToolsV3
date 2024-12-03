using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var identityService = builder.AddProject<Identity_WebApi>("IdentityService");
var userNotificationProcessor = builder.AddProject<UserNotifications_Processor>("UserNotificationProcessor");

builder.Build().Run();
