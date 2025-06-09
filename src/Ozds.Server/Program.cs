using OrchardCore.Logging;
using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Client.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Server.Extensions;
using Ozds.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(
  serverOptions => { serverOptions.Limits.MinRequestBodyDataRate = null; });

if (builder.Environment.IsDevelopment())
{
  builder.Host.UseNLogHost();
}

builder.Services
  .AddOrchardCms()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .ConfigureServices(
    services => services
      .AddOzdsAssets()
      .AddOzdsDocument()
      .AddOzdsReport()
      .AddOzdsUsers()
      .AddOzdsData()
      .AddOzdsMessaging(isDevelopment: builder.Environment.IsDevelopment())
      .AddOzdsJobs()
      .AddOzdsEmail()
      .AddOzdsBusiness()
      .AddOzdsIot()
      .AddOzdsClient()
      .AddOzdsServer(builder))
  .Configure(
    (app, endpoints) => app
      .UseOzdsServer(endpoints));

var app = builder.Build();

app.UseStaticFiles();
app.UseOrchardCore();

await app.RunAsync();
