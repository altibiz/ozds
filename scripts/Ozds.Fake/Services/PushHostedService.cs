using Ozds.Business.Iot;
using Ozds.Fake.Client;
using Ozds.Fake.Generators.Agnostic;

namespace Ozds.Fake.Services;

public class PushHostedService(
  IServiceProvider serviceProvider
) : BackgroundService
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  protected override async Task ExecuteAsync(
    CancellationToken stoppingToken
  )
  {
    var push = _serviceProvider.GetRequiredService<PushOptions>();

    var now = DateTimeOffset.UtcNow;
    var lastPush = now;

    while (true)
    {
      if (stoppingToken.IsCancellationRequested)
      {
        return;
      }

      {
        await using var scope = _serviceProvider.CreateAsyncScope();

        now = DateTimeOffset.UtcNow;

        var pushClient =
          scope.ServiceProvider.GetRequiredService<OzdsPushClient>();
        var generator = scope.ServiceProvider
          .GetRequiredService<AgnosticMeasurementGenerator>();

        var measurements = new List<MessengerPushRequestMeasurement>();
        foreach (var meterId in push.MeterIds)
        {
          measurements.AddRange(
            await generator.GenerateMeasurements(
              lastPush, now, meterId, stoppingToken));
        }

        lastPush = now;

        var request = new MessengerPushRequest(
          now,
          [.. measurements]
        );

        await pushClient.Push(
          push.MessengerId,
          push.ApiKey,
          request,
          stoppingToken
        );
      }

      var toWait =
        TimeSpan.FromSeconds(push.Interval_s)
        - (DateTimeOffset.UtcNow - now);
      if (toWait.TotalMilliseconds > 0)
      {
        await Task.Delay(toWait, stoppingToken);
      }
    }
  }
}
