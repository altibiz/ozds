using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

// TODO: optimize for enumerables

namespace Ozds.Fake.Correction;

public class RecordCorrector(IServiceProvider serviceProvider)
{
  private readonly IServiceProvider _serviceProvider = serviceProvider;

  public IMeasurementRecord CorrectMeterId(
    IMeasurementRecord measurementRecord,
    string meterId
  )
  {
    var corrector = GetCorrector(measurementRecord.GetType());

    return corrector.CorrectMeterId(measurementRecord, meterId);
  }

  public IMeasurementRecord CorrectMeasurementLocationId(
    IMeasurementRecord measurementRecord,
    string measurementLocationId
  )
  {
    var corrector = GetCorrector(measurementRecord.GetType());

    return corrector
      .CorrectMeasurementLocationId(measurementRecord, measurementLocationId);
  }

  public IMeasurementRecord CorrectTimestamp(
    IMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  )
  {
    var corrector = GetCorrector(measurementRecord.GetType());

    return corrector.CorrectTimestamp(measurementRecord, timestamp);
  }

  public IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  )
  {
    var corrector = GetCorrector(measurementRecord.GetType());

    return corrector.CorrectCumulatives(
      timestamp,
      measurementRecord,
      firstMeasurementRecord,
      lastMeasurementRecord
    );
  }

  private IRecordCorrector GetCorrector(Type measurementRecordType)
  {
    var corrector = _serviceProvider.GetServices<IRecordCorrector>()
        .FirstOrDefault(
          corrector =>
            corrector.CanCorrectFor(
              measurementRecordType))
      ?? throw new InvalidOperationException(
        $"No corrector found for {measurementRecordType.Name}");
    return corrector;
  }
}
