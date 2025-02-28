using Ozds.Fake;
using Ozds.Fake.Extensions;
using Ozds.Fake.Services;

var options = OzdsFakeArguments.Parse(args);
if (options is null)
{
  return;
}

var builder = Host.CreateApplicationBuilder();

_ = options switch
{
  OzdsFakePushArguments push => builder.Services
    .AddSingleton(push)
    .AddRecords()
    .AddLoaders()
    .AddGenerators()
    .AddPackers()
    .AddClient(push.Timeout_s, push.BaseUrl)
    .AddHostedService<PushHostedService>(),
  OzdsFakeSeedArguments seed => builder.Services
    .AddSingleton(seed)
    .AddRecords()
    .AddLoaders()
    .AddGenerators()
    .AddPackers()
    .AddClient(seed.Timeout_s, seed.BaseUrl)
    .AddHostedService<SeedHostedService>(),
  OzdsFakeAltibizArguments altibiz => builder.Services
    .AddSingleton(altibiz)
    .AddMessaging(
      altibiz.Host,
      altibiz.VirtualHost,
      altibiz.Username,
      altibiz.Password
    ),
  _ => throw new InvalidOperationException($"Unknown options: {options}")
};

var app = builder.Build();

await app.RunAsync();
