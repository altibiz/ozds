using Ozds.Business.Iot;
using Ozds.Fake;
using Ozds.Fake.Client;
using Ozds.Fake.Extensions;
using Ozds.Fake.Generators.Agnostic;

var options = Options.Parse(args);

var serviceCollection = new ServiceCollection();

serviceCollection.AddLogging(builder => builder.AddConsole());
serviceCollection.AddRecords();
serviceCollection.AddLoaders();
serviceCollection.AddGenerators();
serviceCollection.AddClient(options.Timeout_s, options.BaseUrl);

#pragma warning disable ASP0000
var serviceProvider = serviceCollection.BuildServiceProvider();
#pragma warning restore ASP0000

var now = DateTimeOffset.UtcNow;
var lastPush = now;
while (true)
{
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    now = DateTimeOffset.UtcNow;

    var pushClient = scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
    var generator = scope.ServiceProvider.GetRequiredService<AgnosticMeasurementGenerator>();

    var measurements = new List<MessengerPushRequestMeasurement>();
    foreach (var meterId in options.MeterIds)
    {
      measurements.AddRange(await generator
        .GenerateMeasurements(lastPush, now, meterId));
    }

    lastPush = now;

    var request = new MessengerPushRequest(
      now,
      measurements.ToArray()
    );

    await pushClient.Push(
      options.MessengerId,
      options.ApiKey,
      request
    );
  }

  var toWait =
    TimeSpan.FromSeconds(options.Interval_s)
    - (DateTimeOffset.UtcNow - now);
  if (toWait.TotalMilliseconds > 0)
  {
    await Task.Delay(toWait);
  }
}
