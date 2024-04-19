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

if (options.Seed is { } seed)
{
  await using var scope = serviceProvider.CreateAsyncScope();

  var generator = scope.ServiceProvider
    .GetRequiredService<AgnosticMeasurementGenerator>();

  var seedTimeBegin = seed switch
  {
    Seed.Hour => now.AddHours(-1),
    Seed.Day => now.AddDays(-1),
    Seed.Week => now.AddDays(-7),
    Seed.Month => now.AddMonths(-1),
    Seed.Season => now.AddMonths(-3),
    Seed.Year => now.AddYears(-1),
    _ => throw new InvalidOperationException($"Unknown seed: {seed}")
  };

  while (seedTimeBegin < now)
  {
    var seedTimeEnd = seedTimeBegin.AddDays(1) > now
      ? now
      : seedTimeBegin.AddDays(1);

    var measurements = new List<MessengerPushRequestMeasurement>();
    foreach (var meterId in options.MeterIds)
    {
      measurements.AddRange(await generator
        .GenerateMeasurements(seedTimeBegin, seedTimeEnd, meterId));
    }

    while (measurements.Count > 0)
    {
      var batch = measurements.Take(options.BatchSize).ToList();
      measurements.RemoveRange(0, batch.Count);

      var request = new MessengerPushRequest(
        now,
        batch.ToArray()
      );

      var pushClient = scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
      await pushClient.Push(
        options.MessengerId,
        options.ApiKey,
        request
      );
    }

    seedTimeBegin = seedTimeEnd;
  }

  return;
}

while (true)
{
  {
    await using var scope = serviceProvider.CreateAsyncScope();

    now = DateTimeOffset.UtcNow;

    var pushClient = scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
    var generator = scope.ServiceProvider
      .GetRequiredService<AgnosticMeasurementGenerator>();

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
