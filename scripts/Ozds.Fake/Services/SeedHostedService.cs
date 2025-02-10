using Ozds.Fake.Client;
using Ozds.Fake.Generators.Agnostic;
using Ozds.Fake.Packing.Agnostic;
using Ozds.Iot.Entities.Abstractions;

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
    var seed = _serviceProvider.GetRequiredService<OzdsFakeSeedArguments>();
    var logger =
      _serviceProvider.GetRequiredService<ILogger<SeedHostedService>>();

    var now = DateTimeOffset.UtcNow;

    await using var scope = _serviceProvider.CreateAsyncScope();

    var generator = scope.ServiceProvider
      .GetRequiredService<AgnosticMeasurementGenerator>();
    var packer = scope.ServiceProvider
      .GetRequiredService<AgnosticMessengerPushRequestPacker>();

    var seedTimeBegin = seed.Interval switch
    {
      OzdsFakeSeedInterval.Hour => now.AddHours(-1),
      OzdsFakeSeedInterval.Day => now.AddDays(-1),
      OzdsFakeSeedInterval.Week => now.AddDays(-7),
      OzdsFakeSeedInterval.Month => now.AddMonths(-1),
      OzdsFakeSeedInterval.Season => now.AddMonths(-3),
      OzdsFakeSeedInterval.Year => now.AddYears(-1),
      _ => throw new InvalidOperationException($"Unknown seed: {seed}")
    };

    var measurements = new List<IMeterPushRequestEntity>();
    var seedTimeEnd = seedTimeBegin.AddDays(1) > now
      ? now
      : seedTimeBegin.AddDays(1);
    foreach (var meterId in seed.MeterIds)
    {
      measurements.AddRange(
        await generator.GenerateMeasurements(
          seedTimeBegin, seedTimeEnd, seed.MessengerId, meterId,
          stoppingToken));
    }

    while (seedTimeBegin < now)
    {
      if (stoppingToken.IsCancellationRequested)
      {
        break;
      }

      var pushMeasurements = measurements.ToList();

      async Task GenerateMeasurements()
      {
        measurements.Clear();

        var nextSeedTimeEnd = seedTimeEnd.AddDays(1) > now
          ? now
          : seedTimeEnd.AddDays(1);
        var measurementRanges = new List<List<IMeterPushRequestEntity>>();
        var tasks = new List<Task>();
        foreach (var meterId in seed.MeterIds)
        {
          var measurementRange = new List<IMeterPushRequestEntity>();
          measurementRanges.Add(measurementRange);
          tasks.Add(
            Task.Run(
              async () =>
                measurementRange.AddRange(
                  await generator.GenerateMeasurements(
                    seedTimeEnd, nextSeedTimeEnd, seed.MessengerId, meterId,
                    stoppingToken)), stoppingToken));
        }

        await Task.WhenAll(tasks);

        measurements.AddRange(measurementRanges.SelectMany(x => x));
      }

      async Task PushMeasurements()
      {
        while (pushMeasurements.Count > 0)
        {
          var batch = pushMeasurements.Take(seed.BatchSize).ToList();
          pushMeasurements.RemoveRange(0, batch.Count);

          var request = packer.Pack(
            seed.MessengerId,
            now,
            batch);

          var pushClient =
            scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
          await pushClient.Push(
            seed.MessengerId,
            seed.ApiKey,
            realtime: false,
            request,
            stoppingToken
          );
        }
      }

      try
      {
        await Task.WhenAll(
          GenerateMeasurements(),
          PushMeasurements()
        );
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "Failed to generate and push measurements");
      }

      seedTimeBegin = seedTimeEnd;
      seedTimeEnd = seedTimeBegin.AddDays(1) > now
        ? now
        : seedTimeBegin.AddDays(1);
    }

    var lifetime = _serviceProvider
      .GetRequiredService<IHostApplicationLifetime>();
    lifetime.StopApplication();
  }
}
