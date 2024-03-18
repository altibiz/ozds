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
  .Configure((_, endpoints) => endpoints
    .MapOzdsClient("App", "Index", "/app"));

var app = builder.Build();
app.UseStaticFiles();
app.UseOrchardCore();
await app.RunAsync();
