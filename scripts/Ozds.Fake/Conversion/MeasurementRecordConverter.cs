using System.Runtime.CompilerServices;
using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;
using Ozds.Iot.Entities.Abstractions;

namespace Ozds.Fake.Conversion;

public class MeasurementRecordConverter(
  IServiceProvider serviceProvider)
{
  public IMeterPushRequestEntity ConvertToPushRequest(
    IMeasurementRecord record,
    string messengerId)
  {
    var converter = GetPushRequestConverter(record, messengerId);
    return converter.ConvertToPushRequest(record);
  }

  public IEnumerable<IMeterPushRequestEntity> ConvertToPushRequests(
    IEnumerable<IMeasurementRecord> records,
    string messengerId
  )
  {
    var enumerator = records.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetPushRequestConverter(current, messengerId);

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetPushRequestConverter(next, messengerId);
        current = next;
      }

      yield return converter.ConvertToPushRequest(next);
    }
  }

  public async IAsyncEnumerable<IMeterPushRequestEntity> ConvertToPushRequests(
    IAsyncEnumerable<IMeasurementRecord> records,
    string messengerId,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = records.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync(cancellationToken))
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetPushRequestConverter(current, messengerId);

    while (await enumerator.MoveNextAsync())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetPushRequestConverter(next, messengerId);
        current = next;
      }

      yield return converter.ConvertToPushRequest(next);
    }
  }

  public IMeasurement ConvertToModel(
    IMeasurementRecord record)
  {
    var converter = GetModelConverter(record);
    return converter.ConvertToModel(record);
  }

  public IEnumerable<IMeasurement> ConvertToModels(
    IEnumerable<IMeasurementRecord> records)
  {
    var enumerator = records.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetModelConverter(current);

    while (enumerator.MoveNext())
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetModelConverter(next);
        current = next;
      }

      yield return converter.ConvertToModel(next);
    }
  }

  public async IAsyncEnumerable<IMeasurement> ConvertToModels(
    IAsyncEnumerable<IMeasurementRecord> records,
    [EnumeratorCancellation] CancellationToken cancellationToken
  )
  {
    var enumerator = records.GetAsyncEnumerator(cancellationToken);
    if (!await enumerator.MoveNextAsync(cancellationToken))
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetModelConverter(current);

    while (await enumerator.MoveNextAsync(cancellationToken))
    {
      var next = enumerator.Current;
      if (next.GetType() != current.GetType())
      {
        converter = GetModelConverter(next);
        current = next;
      }

      yield return converter.ConvertToModel(next);
    }
  }

  private IMeasurementRecordPushRequestConverter GetPushRequestConverter(
    IMeasurementRecord record,
    string messengerId
  )
  {
    var converter = serviceProvider
      .GetServices<IMeasurementRecordPushRequestConverter>()
      .FirstOrDefault(c => c.CanConvertToPushRequest(record, messengerId));

    return converter
      ?? throw new InvalidOperationException(
        $"No converter found for {record.GetType()} with messenger {messengerId}");
  }

  private IMeasurementRecordModelConverter GetModelConverter(
    IMeasurementRecord record)
  {
    var converter = serviceProvider
      .GetServices<IMeasurementRecordModelConverter>()
      .FirstOrDefault(c => c.CanConvertToModel(record));

    return converter
      ?? throw new InvalidOperationException(
        $"No converter found for {record.GetType()}");
  }
}
