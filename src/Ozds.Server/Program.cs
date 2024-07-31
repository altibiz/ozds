using OrchardCore.Logging;
using Ozds.Business.Extensions;
using Ozds.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
  builder.Host.UseNLogHost();
}

builder.Services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .ConfigureServices(
    services => services
      .AddOzdsClient(builder)
      .AddOzdsBusinessClient(builder))
  .Configure(
    (_, endpoints) => endpoints
      .MapOzdsClient("App", "Index", "/app")
      .MapOzdsIot("Iot", "Push", "/iot/push")
      .MapOzdsIot("Iot", "Poll", "/iot/poll")
      .MapOzdsIot("Iot", "Update", "/iot/update"))
  .Configure(
    app => app
      .MigrateOzdsData()
      .MigrateOzdsMessaging());

var app = builder.Build();

app.Use(
  async (context, next) =>
  {
    if (context.Request.Path == "/")
    {
      context.Response.Redirect("/app/en", true);
    }
    else
    {
      await next();
    }
  });
app.UseStaticFiles();
app.UseOrchardCore();

await app.RunAsync();
