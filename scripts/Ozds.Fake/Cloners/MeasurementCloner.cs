using System.Runtime.CompilerServices;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Fake.Cloners.Abstractions;
using Ozds.Fake.Identification;

namespace Ozds.Fake.Cloners;

public class MeasurementCloner(
  IServiceProvider serviceProvider
)
{
  public IMeasurement Clone(
    IMeasurement measurement
  )
  {
    var cloner = GetCloner(measurement.GetType());
    return cloner.Clone(measurement);
  }

  public IEnumerable<IMeasurement> Clone(
    IEnumerable<IMeasurement> measurements
  )
  {
    var enumerator = measurements.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var cloner = GetCloner(enumerator.Current.GetType());
    yield return cloner.Clone(current);

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (current.GetType() != next.GetType())
      {
        cloner = GetCloner(next.GetType());
        current = next;
      }

      yield return cloner.Clone(next);
    }
  }

  public async IAsyncEnumerable<IMeasurement> Clone(
    IAsyncEnumerable<IMeasurement> measurements,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = measurements.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current;
    var cloner = GetCloner(enumerator.Current.GetType());
    yield return cloner.Clone(current);

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current;
      if (current.GetType() != next.GetType())
      {
        cloner = GetCloner(next.GetType());
        current = next;
      }

      yield return cloner.Clone(next);
    }
  }

  public IEnumerable<IMeasurement> CloneWith(
    IEnumerable<IMeasurement> measurements,
    IEnumerable<MeasurementLocationMeterId> ids
  )
  {
    var enumerator = measurements.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var cloner = GetCloner(current.GetType());
    var currentIds = ids
      .Where(id => id.MeterModel == current.MeterModel())
      .ToList();

    yield return current;
    foreach (var id in ids)
    {
      yield return Correct(cloner.Clone(current), id);
    }

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (current.GetType() != next.GetType())
      {
        cloner = GetCloner(next.GetType());
        currentIds = ids
          .Where(id => id.MeterModel == next.MeterModel())
          .ToList();
        current = next;
      }

      yield return next;
      foreach (var id in currentIds)
      {
        yield return Correct(cloner.Clone(next), id);
      }
    }
  }

  public async IAsyncEnumerable<IMeasurement> CloneWith(
    IAsyncEnumerable<IMeasurement> measurements,
    IEnumerable<MeasurementLocationMeterId> ids,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = measurements.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync())
    {
      yield break;
    }

    var current = enumerator.Current;
    var cloner = GetCloner(current.GetType());
    var currentIds = ids
      .Where(id => id.MeterModel == current.MeterModel())
      .ToList();

    yield return current;
    foreach (var id in ids)
    {
      yield return Correct(cloner.Clone(current), id);
    }

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current;
      if (current.GetType() != next.GetType())
      {
        cloner = GetCloner(next.GetType());
        currentIds = ids
          .Where(id => id.MeterModel == next.MeterModel())
          .ToList();
        current = next;
      }

      yield return next;
      foreach (var id in currentIds)
      {
        yield return Correct(cloner.Clone(next), id);
      }
    }
  }

  private IMeasurementCloner GetCloner(Type type)
  {
    var cloner = serviceProvider.GetServices<IMeasurementCloner>()
        .FirstOrDefault(cloner => cloner.CanClone(type))
      ?? throw new InvalidOperationException(
        $"No cloner found for {type}");

    return cloner;
  }

  private static IMeasurement Correct(
    IMeasurement model,
    MeasurementLocationMeterId id
  )
  {
    if (model is AggregateModel aggregate)
    {
      aggregate.MeterId = id.MeterId;
      aggregate.MeasurementLocationId = id.MeasurementLocationId;
    }
    else if (model is MeasurementModel measurement)
    {
      measurement.MeterId = id.MeterId;
      measurement.MeasurementLocationId = id.MeasurementLocationId;
    }

    return model;
  }
}
