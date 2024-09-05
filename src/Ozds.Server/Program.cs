using OrchardCore.Logging;
using Ozds.Business.Extensions;
using Ozds.Client.Extensions;
using Ozds.Data.Extensions;
using Ozds.Email.Extensions;
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
      .AddOzdsUsers(builder)
      .AddOzdsData(builder)
      .AddOzdsEmail(builder)
      .AddOzdsMessaging(builder)
      .AddOzdsBusiness(builder)
      .AddOzdsClient(builder)
      .AddOzdsServer(builder))
  .Configure(
    (app, endpoints) => app
      .UseOzdsServer(endpoints)
      .UseOzdsClient(endpoints)
      .UseOzdsBusiness(endpoints)
      .UseOzdsData(endpoints)
      .UseOzdsEmail(endpoints)
      .UseOzdsMessaging(endpoints)
      .UseOzdsUsers(endpoints));

var app = builder.Build();

app.UseStaticFiles();
app.UseOrchardCore();

await app.RunAsync();
