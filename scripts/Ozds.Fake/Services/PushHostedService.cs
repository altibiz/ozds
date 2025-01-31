using Ozds.Fake.Client;
using Ozds.Fake.Generators.Agnostic;
using Ozds.Fake.Packing.Agnostic;
using Ozds.Iot.Entities.Abstractions;

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
    var push = _serviceProvider.GetRequiredService<OzdsFakePushArguments>();

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
        var packer = scope.ServiceProvider
          .GetRequiredService<AgnosticMessengerPushRequestPacker>();

        var measurements = new List<IMeterPushRequestEntity>();
        foreach (var meterId in push.MeterIds)
        {
          measurements.AddRange(
            await generator.GenerateMeasurements(
              lastPush, now, push.MessengerId, meterId, stoppingToken));
        }

        lastPush = now;

        var request = packer.Pack(
          push.MessengerId,
          now,
          measurements);

        await pushClient.Push(
          push.MessengerId,
          push.ApiKey,
          true,
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
