using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot(builder.Configuration);

builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

await app.UseOcelot();
app.Run();
