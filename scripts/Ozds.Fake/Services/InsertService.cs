using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Extensions;
using Ozds.Fake.Identification;
using Ozds.Fake.Services.Base;
using Ozds.Fake.Workers;

namespace Ozds.Fake.Services;

public class InsertService(
  OzdsFakeInsertArguments arguments,
  IServiceProvider services
) : ParallelEnumeratedService<InsertWorkerItem, InsertWorker>(services)
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

      var meters = arguments.Meters.ToList();

      var raw = meters.Count != 0
        ? meters
        : await client.GetMetersForLocation(
          arguments.LocationId,
          cancellationToken);

      ids.AddRange(raw.Select(MeasurementLocationMeterId.FromString));
    }

    await base.StartAsync(cancellationToken);
  }

  // NOTE: parallelize by date and not by meter/measurement location because
  // it takes advantage of cloning measurements for
  // one meter/measurement location to the rest
  protected override IEnumerable<InsertWorkerItem> GetEnumerable()
  {
    var interval = arguments.Interval.ToTimeSpan();
    var dateTo = DateTimeOffset.UtcNow;
    var dateFrom = dateTo.Subtract(interval);
    var dateRange = new DateTimeOffsetRange(dateFrom, dateTo);
    var splitInterval = (dateTo - dateFrom) / Environment.ProcessorCount;
    foreach (var date in dateRange.Split(splitInterval))
    {
      yield return new InsertWorkerItem(
        date.DateFrom,
        date.DateTo,
        ids,
        arguments.BatchSize,
        arguments.AggregatesOnly
      );
    }
  }
}
