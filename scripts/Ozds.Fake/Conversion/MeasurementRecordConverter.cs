using System.Runtime.CompilerServices;
using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Conversion.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Conversion;

public class MeasurementRecordConverter(IServiceProvider serviceProvider)
{
  public IMeasurement ConvertToModel(IMeasurementRecord record)
  {
    var converter = GetModelConverter(record);
    return converter.ConvertToModel(record);
  }

  public IEnumerable<IMeasurement> ConvertToModels(
    IEnumerable<IMeasurementRecord> records
  )
  {
    var enumerator = records.GetEnumerator();
    if (!enumerator.MoveNext())
    {
      yield break;
    }

    var current = enumerator.Current;
    var converter = GetModelConverter(current);

    yield return converter.ConvertToModel(current);

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

    yield return converter.ConvertToModel(current);

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

  private IMeasurementRecordModelConverter GetModelConverter(
    IMeasurementRecord record
  )
  {
    var converter = serviceProvider
      .GetServices<IMeasurementRecordModelConverter>()
      .FirstOrDefault(c => c.CanConvertToModel(record));

    return converter
      ?? throw new InvalidOperationException(
        $"No converter found for {record.GetType()}"
      );
  }
}
