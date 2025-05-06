using Moq;
using Ozds.Assets.Extensions;
using Ozds.Business.Extensions;
using Ozds.Data.Extensions;
using Ozds.Document.Extensions;
using Ozds.Email.Extensions;
using Ozds.Fake.Arguments;
using Ozds.Fake.Extensions;
using Ozds.Iot.Extensions;
using Ozds.Jobs.Extensions;
using Ozds.Messaging.Extensions;
using Ozds.Report.Extensions;
using Ozds.Users.Extensions;
using MessagingMessageSender =
  Ozds.Messaging.Sender.Abstractions.IMessageSender;
using OrchardUserManager =
  Microsoft.AspNetCore.Identity.UserManager<OrchardCore.Users.IUser>;
using OrchardUserStore =
  Microsoft.AspNetCore.Identity.IUserStore<OrchardCore.Users.IUser>;
using YesSqlSession = YesSql.ISession;

var arguments = OzdsFakeArguments.Parse(args);
if (arguments is null)
{
  return 1;
}

var builder = Host.CreateApplicationBuilder();
builder.Services
  .AddOzdsAssets()
  .AddOzdsDocument()
  .AddOzdsReport()
  .AddOzdsUsers()
  .AddOzdsData()
  .AddOzdsMessaging(false)
  .AddOzdsJobs()
  .AddOzdsEmail()
  .AddOzdsBusiness()
  .AddOzdsIot();

// NOTE: hacks to enable most Ozds services working
builder.Services.AddSingleton(Mock.Of<MessagingMessageSender>());
builder.Services.AddSingleton(
  new Mock<OrchardUserManager>(
    Mock.Of<OrchardUserStore>(), null!, null!, null!, null!, null!, null!,
    null!, null!).Object);
builder.Services.AddSingleton(Mock.Of<YesSqlSession>());
foreach (var service in builder.Services
  .Where(
    service =>
      service.ServiceType == typeof(IHostedService)
      && !(service.ImplementationInstance?.GetType().Namespace
        ?.StartsWith(nameof(Microsoft)) ?? false)
      && !(service.ImplementationType?.Namespace
        ?.StartsWith(nameof(Microsoft)) ?? false)
      && !(service.ImplementationFactory?.Method?.Module.Name
        ?.StartsWith(nameof(Microsoft)) ?? false))
  .ToList())
{
  builder.Services.Remove(service);
}

builder.Services
  .AddOzdsFake(arguments);

var app = builder.Build();
await app.RunAsync();

return 0;
