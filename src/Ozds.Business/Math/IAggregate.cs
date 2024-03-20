using System.Linq.Expressions;

namespace Ozds.Business.Math;

public interface IAggregate<T> : IAggregate where T : IAggregate<T>
{
  public static abstract Expression<Func<T, T, T>> UpsertExpression { get; }

  public static Func<T, T, T> Upsert => T.UpsertExpression.Compile();
}

public interface IAggregate : IMeasurement
{
  public TimeSpan TimeSpan { get; }

  public long AggregateCount { get; }

  public SpanningMeasure<DuplexMeasure> ActiveEnergySpan_Wh { get; }

  public SpanningMeasure<DuplexMeasure> ReactiveEnergySpan_VARh { get; }

  public SpanningMeasure<DuplexMeasure> ApparentEnergySpan_VAh { get; }

  public SpanningMeasure<TariffMeasure> TariffEnergySpan_Wh { get; }

  DuplexMeasure IMeasurement.ActivePower_W
  {
    get
    {
      return ActiveEnergySpan_Wh.SpanDifferential((float)TimeSpan.TotalHours);
    }
  }

  DuplexMeasure IMeasurement.ReactivePower_VAR
  {
    get
    {
      return ReactiveEnergySpan_VARh.SpanDifferential(
        (float)TimeSpan.TotalHours);
    }
  }

  DuplexMeasure IMeasurement.ApparentPower_VA
  {
    get
    {
      return ApparentEnergySpan_VAh.SpanDifferential(
        (float)TimeSpan.TotalHours);
    }
  }

  DuplexMeasure IMeasurement.ActiveEnergyCumulative_Wh
  {
    get { return ActiveEnergySpan_Wh.SpanMax; }
  }

  DuplexMeasure IMeasurement.ReactiveEnergyCumulative_VARh
  {
    get { return ReactiveEnergySpan_VARh.SpanMax; }
  }

  DuplexMeasure IMeasurement.ApparentEnergyCumulative_VAh
  {
    get { return ApparentEnergySpan_VAh.SpanMax; }
  }

  TariffMeasure IMeasurement.TariffEnergyCumulative_Wh
  {
    get { return TariffEnergySpan_Wh.SpanMax; }
  }
}

public static class IAggregateExtensions
{
  public static IEnumerable<T> UpsertRange<T>(this IEnumerable<T> aggregates)
    where T : IAggregate<T> =>
    aggregates
      .GroupBy(aggregate => (aggregate.Source, aggregate.Timestamp, aggregate.TimeSpan))
      .Select(group => group.Aggregate((a, b) => IAggregate<T>.Upsert(a, b)));
}
