using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Business.Conversion;

public record MeterPushRequestWithMeasurementLocationId(
  IMeterPushRequestEntity MeterPushRequest,
  string MeasurementLocationId
);

public class PushRequestMeasurementConverter(
  IServiceProvider serviceProvider
)
{
  private readonly ConcurrentDictionary<Type, IPushRequestMeasurementConverter>
    measurementCache = new();

  private readonly ConcurrentDictionary<Type, IPushRequestMeasurementConverter>
    pushRequestCache = new();

  public TPushRequest ToPushRequest<TPushRequest>(
    IMeasurement measurement
  )
  {
    return (TPushRequest)ToPushRequest(measurement);
  }

  public IEnumerable<TPushRequest> ToPushRequests<TPushRequest>(
    IEnumerable<IMeasurement> measurements
  )
  {
    return ToPushRequests(measurements).OfType<TPushRequest>();
  }

  public IAsyncEnumerable<TPushRequest> ToPushRequests<TPushRequest>(
    IAsyncEnumerable<IMeasurement> measurements,
    CancellationToken cancellationToken
  )
  {
    return ToPushRequests(measurements, cancellationToken)
      .OfType<TPushRequest>();
  }

  public IMeterPushRequestEntity ToPushRequest(IMeasurement measurement)
  {
    return GetMeasurementConverter(measurement.GetType())
      .ToPushRequest(measurement);
  }

  public IEnumerable<IMeterPushRequestEntity> ToPushRequests(
    IEnumerable<IMeasurement> measurements
  )
  {
    var enumerator = measurements.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetMeasurementConverter(current.GetType());

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetMeasurementConverter(next.GetType());
        current = next;
      }

      yield return converter.ToPushRequest(next);
    }
  }

  public async IAsyncEnumerable<IMeterPushRequestEntity> ToPushRequests(
    IAsyncEnumerable<IMeasurement> models,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = models.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync(cancellationToken))
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetMeasurementConverter(current.GetType());

    while (await enumerator.MoveNextAsync(cancellationToken))
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetMeasurementConverter(next.GetType());
        current = next;
      }

      yield return converter.ToPushRequest(next);
    }
  }

  public TMeasurement ToMeasurement<TMeasurement>(
    MeterPushRequestWithMeasurementLocationId pushRequest
  )
    where TMeasurement : IMeasurement
  {
    return (TMeasurement)ToMeasurement(pushRequest);
  }

  public IEnumerable<TMeasurement> ToMeasurements<TMeasurement>(
    IEnumerable<MeterPushRequestWithMeasurementLocationId> pushRequests
  )
  {
    return ToMeasurements(pushRequests).OfType<TMeasurement>();
  }

  public IAsyncEnumerable<TMeasurement> ToMeasurements<TMeasurement>(
    IAsyncEnumerable<MeterPushRequestWithMeasurementLocationId> pushRequests,
    CancellationToken cancellationToken
  )
  {
    return ToMeasurements(pushRequests, cancellationToken)
      .OfType<TMeasurement>();
  }

  public IMeasurement ToMeasurement(
    MeterPushRequestWithMeasurementLocationId pushRequest
  )
  {
    return GetPushRequestConverter(pushRequest.GetType())
      .ToMeasurement(
        pushRequest.MeterPushRequest,
        pushRequest.MeasurementLocationId);
  }

  public IEnumerable<IMeasurement> ToMeasurements(
    IEnumerable<MeterPushRequestWithMeasurementLocationId> pushRequests
  )
  {
    var enumerator = pushRequests.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetPushRequestConverter(current.GetType());

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetPushRequestConverter(next.GetType());
        current = next;
      }

      yield return converter.ToMeasurement(
        next.MeterPushRequest,
        next.MeasurementLocationId);
    }
  }

  public async IAsyncEnumerable<IMeasurement> ToMeasurements(
    IAsyncEnumerable<MeterPushRequestWithMeasurementLocationId> pushRequests,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = pushRequests.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync(cancellationToken))
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetPushRequestConverter(current.GetType());

    while (await enumerator.MoveNextAsync(cancellationToken))
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetPushRequestConverter(next.GetType());
        current = next;
      }

      yield return converter.ToMeasurement(
        next.MeterPushRequest,
        next.MeasurementLocationId);
    }
  }

  private IPushRequestMeasurementConverter GetPushRequestConverter(
    Type pushRequestType
  )
  {
    if (pushRequestCache.TryGetValue(pushRequestType, out var converter))
    {
      return converter;
    }

    converter = serviceProvider
        .GetServices<IPushRequestMeasurementConverter>()
        .FirstOrDefault(
          converter =>
            converter.PushRequestType.IsAssignableTo(pushRequestType))
      ?? throw new InvalidOperationException(
        $"No converter found for {pushRequestType.Name}");

    pushRequestCache.TryAdd(pushRequestType, converter);

    return converter;
  }

  private IPushRequestMeasurementConverter GetMeasurementConverter(
    Type measurementType
  )
  {
    if (measurementCache.TryGetValue(measurementType, out var converter))
    {
      return converter;
    }

    converter = serviceProvider
        .GetServices<IPushRequestMeasurementConverter>()
        .FirstOrDefault(
          converter =>
            converter.MeasurementType.IsAssignableTo(measurementType))
      ?? throw new InvalidOperationException(
        $"No converter found for {measurementType.Name}");

    measurementCache.TryAdd(measurementType, converter);

    return converter;
  }
}
