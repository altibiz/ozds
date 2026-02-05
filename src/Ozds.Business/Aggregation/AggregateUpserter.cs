using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Ozds.Business.Aggregation.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Aggregation;

public class AggregateUpserter(IServiceProvider serviceProvider)
{
  private readonly ConcurrentDictionary<
    (Type, Type),
    IAggregateUpserter
  > cache = new();

  public TModel UpsertAggregate<TModel>(TModel lhs, TModel rhs)
    where TModel : IAggregate
  {
    var upserter = GetUpserter(lhs.GetType(), rhs.GetType());
    return (TModel)upserter.Upsert(lhs, rhs);
  }

  public IEnumerable<TModel> UpsertAggregates<TModel>(
    IEnumerable<TModel> models
  )
    where TModel : IAggregate
  {
    var enumerator = models.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var upserter = GetUpserter(current.GetType(), current.GetType());
    var currents = new List<TModel?>();
    currents.AddRange(
      Enumerable
        .Range(0, Enum.GetValues<IntervalModel>().Select(x => (int)x).Max() + 1)
        .Select(_ => (TModel?)default)
    );
    currents[(int)current.Interval] = current;

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      current = currents[(int)next.Interval];
      if (current is not { } nonNullCurrent)
      {
        currents[(int)next.Interval] = next;
        continue;
      }

      if (next.GetType() == nonNullCurrent.GetType())
      {
        if (
          next.Timestamp == nonNullCurrent.Timestamp
          && next.MeterId == nonNullCurrent.MeterId
          && next.MeasurementLocationId == nonNullCurrent.MeasurementLocationId
        )
        {
          currents[(int)next.Interval] = (TModel)
            upserter.Upsert(nonNullCurrent, next);
        }
        else
        {
          yield return current;
          currents[(int)next.Interval] = next;
        }
      }
      else
      {
        yield return current;
        currents[(int)next.Interval] = next;
        upserter = GetUpserter(next.GetType(), next.GetType());
      }
    }

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
    foreach (var next in currents)
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    {
      if (next is not null)
      {
        yield return next;
      }
    }
  }

  public async IAsyncEnumerable<TModel> UpsertAggregates<TModel>(
    IAsyncEnumerable<TModel> models,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
    where TModel : IAggregate
  {
    var enumerator = models.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current;
    var upserter = GetUpserter(current.GetType(), current.GetType());
    var currents = new List<TModel?>();
    currents.AddRange(
      Enumerable
        .Range(0, Enum.GetValues<IntervalModel>().Select(x => (int)x).Max() + 1)
        .Select(_ => (TModel?)default)
    );
    currents[(int)current.Interval] = current;

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current;
      current = currents[(int)next.Interval];
      if (current is not { } nonNullCurrent)
      {
        currents[(int)next.Interval] = next;
        continue;
      }

      if (next.GetType() == nonNullCurrent.GetType())
      {
        if (
          next.Timestamp == nonNullCurrent.Timestamp
          && next.MeterId == nonNullCurrent.MeterId
          && next.MeasurementLocationId == nonNullCurrent.MeasurementLocationId
        )
        {
          currents[(int)next.Interval] = (TModel)
            upserter.Upsert(nonNullCurrent, next);
        }
        else
        {
          yield return current;
          currents[(int)next.Interval] = next;
        }
      }
      else
      {
        yield return current;
        currents[(int)next.Interval] = next;
        upserter = GetUpserter(next.GetType(), next.GetType());
      }
    }

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
    foreach (var next in currents)
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    {
      if (next is not null)
      {
        yield return next;
      }
    }
  }

  public IEnumerable<IMeasurement> UpsertMeasurements(
    IEnumerable<IMeasurement> models
  )
  {
    var enumerator = models.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var currentOrMeasurement = enumerator.Current;
    if (currentOrMeasurement is not IAggregate)
    {
      yield return currentOrMeasurement;
      while (enumerator.MoveNext())
      {
        currentOrMeasurement = enumerator.Current;
        if (currentOrMeasurement is not IAggregate)
        {
          yield return currentOrMeasurement;
        }
        else
        {
          break;
        }
      }
    }

    var current = (IAggregate)currentOrMeasurement;
    var upserter = GetUpserter(current.GetType(), current.GetType());
    var currents = new List<IAggregate?>();
    currents.AddRange(
      Enumerable
        .Range(0, Enum.GetValues<IntervalModel>().Select(x => (int)x).Max() + 1)
        .Select(_ => (IAggregate?)default)
    );
    currents[(int)current.Interval] = current;

    while (enumerator.MoveNext())
    {
      currentOrMeasurement = enumerator.Current;
      if (currentOrMeasurement is not IAggregate)
      {
        yield return currentOrMeasurement;
        continue;
      }

      var next = (IAggregate)currentOrMeasurement;
      current = currents[(int)next.Interval];
      if (current is not { } nonNullCurrent)
      {
        currents[(int)next.Interval] = next;
        continue;
      }

      if (next.GetType() == nonNullCurrent.GetType())
      {
        if (
          next.Timestamp == nonNullCurrent.Timestamp
          && next.MeterId == nonNullCurrent.MeterId
          && next.MeasurementLocationId == nonNullCurrent.MeasurementLocationId
        )
        {
          currents[(int)next.Interval] = upserter.Upsert(nonNullCurrent, next);
        }
        else
        {
          yield return current;
          currents[(int)next.Interval] = next;
        }
      }
      else
      {
        yield return current;
        currents[(int)next.Interval] = next;
        upserter = GetUpserter(next.GetType(), next.GetType());
      }
    }

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
    foreach (var next in currents)
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    {
      if (next is not null)
      {
        yield return next;
      }
    }
  }

  public async IAsyncEnumerable<IMeasurement> UpsertMeasurements(
    IAsyncEnumerable<IMeasurement> models,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = models.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var currentOrMeasurement = enumerator.Current;
    if (currentOrMeasurement is not IAggregate)
    {
      yield return currentOrMeasurement;
      while (await enumerator.MoveNextAsync())
      {
        currentOrMeasurement = enumerator.Current;
        if (currentOrMeasurement is not IAggregate)
        {
          yield return currentOrMeasurement;
        }
        else
        {
          break;
        }
      }
    }

    var current = (IAggregate)currentOrMeasurement;
    var upserter = GetUpserter(current.GetType(), current.GetType());
    var currents = new List<IAggregate?>();
    currents.AddRange(
      Enumerable
        .Range(0, Enum.GetValues<IntervalModel>().Select(x => (int)x).Max() + 1)
        .Select(_ => (IAggregate?)default)
    );
    currents[(int)current.Interval] = current;

    while (await enumerator.MoveNextAsync())
    {
      currentOrMeasurement = enumerator.Current;
      if (currentOrMeasurement is not IAggregate)
      {
        yield return currentOrMeasurement;
        continue;
      }

      var next = (IAggregate)currentOrMeasurement;
      current = currents[(int)next.Interval];
      if (current is not { } nonNullCurrent)
      {
        currents[(int)next.Interval] = next;
        continue;
      }

      if (next.GetType() == nonNullCurrent.GetType())
      {
        if (
          next.Timestamp == nonNullCurrent.Timestamp
          && next.MeterId == nonNullCurrent.MeterId
          && next.MeasurementLocationId == nonNullCurrent.MeasurementLocationId
        )
        {
          currents[(int)next.Interval] = upserter.Upsert(nonNullCurrent, next);
        }
        else
        {
          yield return current;
          currents[(int)next.Interval] = next;
        }
      }
      else
      {
        yield return current;
        currents[(int)next.Interval] = next;
        upserter = GetUpserter(next.GetType(), next.GetType());
      }
    }

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
    foreach (var next in currents)
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions
    {
      if (next is not null)
      {
        yield return next;
      }
    }
  }

  private IAggregateUpserter GetUpserter(Type lhsType, Type rhsType)
  {
    if (cache.TryGetValue((lhsType, rhsType), out var upserter))
    {
      return upserter;
    }

    upserter =
      serviceProvider
        .GetServices<IAggregateUpserter>()
        .FirstOrDefault(upserter =>
          upserter.CanUpsert(lhsType) && upserter.CanUpsert(rhsType)
        )
      ?? throw new InvalidOperationException(
        $"No upserter found for models {lhsType} and {rhsType}."
      );

    cache.TryAdd((lhsType, rhsType), upserter);

    return upserter;
  }
}
