using OrchardCore.Logging;
using Ozds.Business.Extensions;
using Ozds.Client.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Server.Extensions;
using Ozds.Users.Extensions;

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
      .AddOzdsDocument(builder)
      .AddOzdsUsers(builder)
      .AddOzdsData(builder)
      .AddOzdsMessaging(builder)
      .AddOzdsJobs(builder)
      .AddOzdsEmail(builder)
      .AddOzdsBusiness(builder)
      .AddOzdsIot(builder)
      .AddOzdsClient(builder)
      .AddOzdsServer(builder))
  .Configure(
    (app, endpoints) => app
      .UseOzdsServer(endpoints)
      .UseOzdsClient(endpoints)
      .UseOzdsIot(endpoints)
      .UseOzdsBusiness(endpoints)
      .UseOzdsEmail(endpoints)
      .UseOzdsJobs(endpoints)
      .UseOzdsMessaging(endpoints)
      .UseOzdsData(endpoints)
      .UseOzdsUsers(endpoints)
      .UseOzdsDocument(endpoints));

var app = builder.Build();

app.UseStaticFiles();
app.UseOrchardCore();

await app.RunAsync();
