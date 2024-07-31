using Ozds.Business.Iot;
using Ozds.Fake.Client;
using Ozds.Fake.Generators.Agnostic;

namespace Ozds.Fake.Services;

public class SeedHostedService(
  IServiceProvider serviceProvider
) : BackgroundService
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  protected override async Task ExecuteAsync(
    CancellationToken stoppingToken
  )
  {
    var seed = _serviceProvider.GetRequiredService<SeedOptions>();

    var now = DateTimeOffset.UtcNow;

    await using var scope = _serviceProvider.CreateAsyncScope();

    var generator = scope.ServiceProvider
      .GetRequiredService<AgnosticMeasurementGenerator>();

    var seedTimeBegin = seed.Interval switch
    {
      SeedInterval.Hour => now.AddHours(-1),
      SeedInterval.Day => now.AddDays(-1),
      SeedInterval.Week => now.AddDays(-7),
      SeedInterval.Month => now.AddMonths(-1),
      SeedInterval.Season => now.AddMonths(-3),
      SeedInterval.Year => now.AddYears(-1),
      _ => throw new InvalidOperationException($"Unknown seed: {seed}")
    };

    while (seedTimeBegin < now)
    {
      var seedTimeEnd = seedTimeBegin.AddDays(1) > now
        ? now
        : seedTimeBegin.AddDays(1);

      var measurements = new List<MessengerPushRequestMeasurement>();
      foreach (var meterId in seed.MeterIds)
      {
        measurements.AddRange(
          await generator.GenerateMeasurements(
            seedTimeBegin, seedTimeEnd, meterId, stoppingToken));
      }

      while (measurements.Count > 0)
      {
        var batch = measurements.Take(seed.BatchSize).ToList();
        measurements.RemoveRange(0, batch.Count);

        var request = new MessengerPushRequest(
          now,
          [.. batch]
        );

        var pushClient =
          scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
        await pushClient.Push(
          seed.MessengerId,
          seed.ApiKey,
          request,
          stoppingToken
        );
      }

      seedTimeBegin = seedTimeEnd;
    }
  }
}
