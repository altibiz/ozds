using Ozds.Business.Services.Base;
using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Extensions;
using Ozds.Fake.Identification;
using Ozds.Fake.Workers;

namespace Ozds.Fake.Services;

public class PushService(
  IServiceProvider services,
  OzdsFakePushArguments arguments
) : AsyncEnumeratedService<PushWorkerItem, PushWorker>(services)
{
  private readonly List<MeasurementLocationMeterId> ids = new();
  private readonly IServiceProvider services = services;

  public override async Task StartAsync(
    CancellationToken cancellationToken
  )
  {
    {
      await using var scope = services.CreateAsyncScope();

      var client = scope.ServiceProvider
        .GetRequiredService<InsertClient>();

      var meterIds = arguments.MeterIds.ToList();

      var raw = meterIds.Count != 0
        ? meterIds.Select(id => $"0:{id}").ToList()
        : await client.GetMetersForLocation(
          arguments.LocationId,
          cancellationToken);

      ids.AddRange(raw.Select(MeasurementLocationMeterId.FromString));
    }

    await base.StartAsync(cancellationToken);
  }

  protected override IAsyncEnumerable<PushWorkerItem> GetEnumerable(
    CancellationToken cancellationToken
  )
  {
    return TimeSpan.FromSeconds(arguments.Interval_s)
      .Future(cancellationToken)
      .Select(
        range => new PushWorkerItem(
          range.DateFrom,
          range.DateTo,
          arguments.MessengerId,
          ids,
          10000,
          arguments.Realtime ? "realtime" : "buffer"
        ));
  }
}
