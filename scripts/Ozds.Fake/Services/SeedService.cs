using Ozds.Business.Services.Base;
using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Identification;
using Ozds.Fake.Workers;

namespace Ozds.Fake.Services;

public class SeedService(
  OzdsFakeSeedArguments arguments,
  IServiceProvider services
) : EnumeratedService<PushWorkerItem, PushWorker>(services)
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

  protected override IEnumerable<PushWorkerItem> GetEnumerable()
  {
    var interval = arguments.Interval.ToTimeSpan();
    var dateTo = DateTimeOffset.UtcNow;
    var dateFrom = dateTo.Subtract(interval);
    yield return new PushWorkerItem(
      dateFrom,
      dateTo,
      arguments.MessengerId,
      ids,
      BatchSize: arguments.BatchSize,
      BufferBehavior: "aggregate"
    );
  }
}
