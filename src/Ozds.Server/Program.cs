using Ozds.Business.Extensions;
using Ozds.Client.Extensions;
using Ozds.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .ConfigureServices(services => services
    .AddOzdsClient(builder.Environment.IsDevelopment())
    .AddOzdsBusinessClient()
    .AddOzdsDataClient())
  .Configure((_, endpoints) => endpoints.MapBlazorHub("/app/_blazor"))
  .Configure((_, endpoints) => endpoints
    .MapAreaControllerRoute(
      name: "App",
      areaName: "Ozds.Server",
      pattern: "/app/{**catchall}",
      defaults: new { controller = "Client", action = "Index" }
    ));

var app = builder.Build();
app.UseOrchardCore();
await app.RunAsync();
