using Ozds.Business.Extensions;
using Ozds.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .ConfigureServices(services => services
    .AddOzdsClient(builder.Environment.IsDevelopment())
    .AddOzdsBusinessClient(builder.Environment.IsDevelopment()))
  .Configure((_, endpoints) => endpoints
    .MapOzdsClient("App", "Index", "/app")
    .MapOzdsIot("Iot", "Push", "/iot"))
  .Configure(app => app
    .MigrateOzdsData());


var app = builder.Build();

app.Use(async (context, next) =>
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
