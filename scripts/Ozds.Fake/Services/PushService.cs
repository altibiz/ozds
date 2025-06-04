using System.Runtime.CompilerServices;
using Ozds.Business.Queries;
using Ozds.Fake.Arguments;
using Ozds.Fake.Client;
using Ozds.Fake.Identification;
using Ozds.Fake.Services.Base;
using Ozds.Fake.Workers;

namespace Ozds.Fake.Services;

public class PushService(
  IServiceProvider services,
  OzdsFakePushArguments arguments,
  ClockQueries clock
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
    return Future(TimeSpan.FromSeconds(arguments.Interval_s), cancellationToken)
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

  private async IAsyncEnumerable<DateTimeOffsetRange> Future(
    TimeSpan interval,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var dateTo = clock.Now();
    while (true)
    {
      if (cancellationToken.IsCancellationRequested)
      {
        break;
      }

      await Task.Delay(interval, cancellationToken);
      var dateFrom = dateTo;
      dateTo = clock.Now();
      yield return new DateTimeOffsetRange(dateFrom, dateTo);
    }
  }

  private sealed record DateTimeOffsetRange(
    DateTimeOffset DateFrom,
    DateTimeOffset DateTo
  );
}
