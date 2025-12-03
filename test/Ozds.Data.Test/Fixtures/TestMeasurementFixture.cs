using Microsoft.EntityFrameworkCore;
using Ozds.Data.Context;
using Ozds.Data.Entities.Abstractions;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Reflection;
using Ozds.Data.Test.Extensions;
using Ozds.Time.Queries.Abstractions;
using TimeIntervalEntity = Ozds.Time.Entities.IntervalEntity;

namespace Ozds.Data.Test.Fixtures;

public class TestMeasurementFixture(
  IDbContextFactory<DataDbContext> factory,
  EntityReflector reflector,
  ITimeQueries time
)
{
  public async Task<List<IMeasurementEntity>> Create(
    InfrastructureEntities infrastructure,
    CancellationToken cancellationToken,
    Action<Configurator>? configure = null
  )
  {
    var configurator = new Configurator();
    configure?.Invoke(configurator);

    await using var context = await factory
      .CreateDbContextAsync(cancellationToken);
    var fixture = context.ContextualFixture();

    var type = reflector.ResolveMeterMeasurementType(
      infrastructure.Meter.GetType(),
      configurator.Interval is not null);

    // NOTE: trick to get start of an interval not before from
    var fromDate = configurator.Interval is null
      ? configurator.FromDate
      : configurator.Interval is IntervalEntity.QuarterHour
        ? time.GetStartOfQuarterHour(
          configurator.FromDate
            .AddMinutes(15).AddTicks(-1))
        : configurator.Interval is IntervalEntity.Day
          ? time.GetStartOfDay(
            configurator.FromDate
              .AddDays(1).AddTicks(-1))
          : time.GetStartOfMonth(
            configurator.FromDate
              .AddMonths(1).AddTicks(-1));

    var toDate = configurator.Interval is null
      ? configurator.ToDate
      : configurator.Interval is IntervalEntity.QuarterHour
        ? time.GetStartOfQuarterHour(configurator.ToDate)
        : configurator.Interval is IntervalEntity.Day
          ? time.GetStartOfDay(configurator.ToDate)
          : time.GetStartOfMonth(configurator.ToDate);

    var timeInterval = configurator.Interval is null
      ? (TimeIntervalEntity?)null
      : configurator.Interval is IntervalEntity.QuarterHour
        ? TimeIntervalEntity.QuarterHour
        : configurator.Interval is IntervalEntity.Day
          ? TimeIntervalEntity.Day
          : TimeIntervalEntity.Month;

    var timeSpan = timeInterval is not null
      ? time.IntervalTimeSpan(timeInterval.Value, fromDate)
      : (TimeSpan?)null;

    var count = timeSpan is null
      ? configurator.Count
      : Math.Min(
        configurator.Count,
        (int)Math.Floor((toDate - fromDate) / timeSpan.Value));

    var measurements = fixture
      .CreateMany<IMeasurementEntity>(type, count)
      .ToList();

    foreach (var (item, index) in measurements.Select((x, i) => (x, i)))
    {
      if (item is MeasurementEntity measurement)
      {
        measurement.MeterId = infrastructure.Meter.Id;
        measurement.MeasurementLocationId =
          infrastructure.MeasurementLocation.Id;
        measurement.Timestamp = fromDate + (toDate - fromDate) * index / count;
      }

      if (item is AggregateEntity aggregate && timeSpan is not null)
      {
        aggregate.MeterId = infrastructure.Meter.Id;
        aggregate.MeasurementLocationId =
          infrastructure.MeasurementLocation.Id;
        aggregate.Timestamp = fromDate + timeSpan.Value * index;
        aggregate.Interval = configurator.Interval
          ?? IntervalEntity.QuarterHour;

        // NOTE: matches app behavior and ensures correct upsert
        if (aggregate.Interval is IntervalEntity.QuarterHour)
        {
          aggregate.Count = 1;
          aggregate.QuarterHourCount = 1;
        }
        else
        {
          aggregate.Count = 1;
          aggregate.QuarterHourCount = 0;
        }
      }
    }

    return measurements;
  }

  public class Configurator
  {
    public IntervalEntity? Interval { get; private set; }

    public DateTimeOffset FromDate { get; private set; } =
      Constants.NowStartOfMonth;

    public DateTimeOffset ToDate { get; private set; } =
      Constants.Now;

    public int Count { get; private set; } = 1;

    public Configurator WithInterval(IntervalEntity? interval)
    {
      Interval = interval;
      return this;
    }

    public Configurator WithFromDate(DateTimeOffset fromDate)
    {
      FromDate = fromDate;
      return this;
    }

    public Configurator WithToDate(DateTimeOffset toDate)
    {
      ToDate = toDate;
      return this;
    }

    public Configurator WithCount(int count)
    {
      Count = count;
      return this;
    }
  }
}
