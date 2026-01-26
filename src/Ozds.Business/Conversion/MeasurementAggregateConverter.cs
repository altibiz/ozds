using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Conversion;

public class MeasurementAggregateConverter(
  IServiceProvider serviceProvider)
{
  private readonly
    ConcurrentDictionary<Type, IMeasurementAggregateConverter> cache = new();

  public IAggregate ToAggregate(
    IMeasurement model,
    IntervalModel interval)
  {
    return ToAggregate<IAggregate>(model, interval);
  }

  public TAggregate ToAggregate<TAggregate>(
    IMeasurement model,
    IntervalModel interval)
    where TAggregate : class, IAggregate
  {
    var converter = GetConverter(model.GetType());
    return (TAggregate)converter.ToAggregate(model, interval);
  }

  public IEnumerable<IAggregate> ToAggregates(
    IEnumerable<IMeasurement> measurements
  )
  {
    return ToAggregates<IAggregate>(measurements);
  }

  public IEnumerable<TAggregate> ToAggregates<TAggregate>(
    IEnumerable<IMeasurement> measurements
  )
    where TAggregate : IAggregate
  {
    var enumerator = measurements.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current!;
    var converter = GetConverter(current.GetType());

    foreach (var interval in Enum.GetValues<IntervalModel>())
    {
      yield return (TAggregate)converter.ToAggregate(current, interval);
    }

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current!;
      if (next.GetType() != current.GetType())
      {
        converter = GetConverter(next.GetType());
        current = next;
      }

      foreach (var interval in Enum.GetValues<IntervalModel>())
      {
        yield return (TAggregate)converter.ToAggregate(next, interval);
      }
    }
  }

  public IAsyncEnumerable<IAggregate> ToAggregates(
    IAsyncEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    return ToAggregates<IAggregate>(measurements, cancellationToken);
  }

  public async IAsyncEnumerable<TAggregate> ToAggregates<TAggregate>(
    IAsyncEnumerable<IMeasurement> measurements,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
    where TAggregate : IAggregate
  {
    var enumerator = measurements.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current!;
    var converter = GetConverter(current.GetType());

    foreach (var interval in Enum.GetValues<IntervalModel>())
    {
      yield return (TAggregate)converter.ToAggregate(current, interval);
    }

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current!;
      if (next.GetType() != current.GetType())
      {
        converter = GetConverter(next.GetType());
        current = next;
      }

      foreach (var interval in Enum.GetValues<IntervalModel>())
      {
        yield return (TAggregate)converter.ToAggregate(next, interval);
      }
    }
  }

  public IEnumerable<IMeasurement> WithAggregates(
    IEnumerable<IMeasurement> measurements
  )
  {
    var enumerator = measurements.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current!;
    var converter = GetConverter(current.GetType());

    yield return current;
    foreach (var interval in Enum.GetValues<IntervalModel>())
    {
      yield return converter.ToAggregate(current, interval);
    }

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current!;
      if (next.GetType() != current.GetType())
      {
        converter = GetConverter(next.GetType());
        current = next;
      }

      yield return next;
      foreach (var interval in Enum.GetValues<IntervalModel>())
      {
        yield return converter.ToAggregate(next, interval);
      }
    }
  }

  public async IAsyncEnumerable<IMeasurement> WithAggregates(
    IAsyncEnumerable<IMeasurement> measurements,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = measurements.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current!;
    var converter = GetConverter(current.GetType());

    yield return current;
    foreach (var interval in Enum.GetValues<IntervalModel>())
    {
      yield return converter.ToAggregate(current, interval);
    }

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current!;
      if (next.GetType() != current.GetType())
      {
        converter = GetConverter(next.GetType());
        current = next;
      }

      yield return next;
      foreach (var interval in Enum.GetValues<IntervalModel>())
      {
        yield return converter.ToAggregate(next, interval);
      }
    }
  }

  private IMeasurementAggregateConverter GetConverter(Type type)
  {
    if (cache.TryGetValue(type, out var converter))
    {
      return converter;
    }

    converter = serviceProvider
        .GetServices<IMeasurementAggregateConverter>()
        .FirstOrDefault(converter =>
          converter.CanConvertToAggregate(type))
      ?? throw new InvalidOperationException(
        $"No converter found for measurement type {type}.");

    cache.TryAdd(type, converter);

    return converter;
  }
}
