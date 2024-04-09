using Ozds.Business.Extensions;
using Ozds.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .ConfigureServices(services => services
    .AddOzdsClient(builder.Environment.IsDevelopment())
    .AddOzdsBusinessClient())
  .Configure((_, endpoints) => endpoints
    .MapOzdsClient("App", "Index", "/app")
    .MapOzdsIot("Iot", "Push", "/iot"))
  .Configure(app => app
    .MigrateOzdsData());

var app = builder.Build();
app.UseStaticFiles();
app.UseOrchardCore();
await app.RunAsync();
