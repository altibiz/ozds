using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Base;

public abstract class
  RecordCorrector<TMeasurementRecord> : IRecordCorrector
  where TMeasurementRecord : class, IMeasurementRecord
{
  public bool CanCorrectFor(Type measurementRecordType)
  {
    return measurementRecordType.IsAssignableFrom(typeof(TMeasurementRecord));
  }

  public IMeasurementRecord CorrectMeterId(
    IMeasurementRecord measurementRecord,
    string meterId
  )
  {
    return CorrectMeterId(
      CastRecord(measurementRecord),
      meterId
    );
  }

  public IMeasurementRecord CorrectMeasurementLocationId(
    IMeasurementRecord measurementRecord,
    string measurementLocationId
  )
  {
    return CorrectMeasurementLocationId(
      CastRecord(measurementRecord),
      measurementLocationId
    );
  }

  public IMeasurementRecord CorrectTimestamp(
    IMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  )
  {
    return CorrectTimestamp(
      CastRecord(measurementRecord),
      timestamp
    );
  }

  public IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  )
  {
    return CorrectCumulatives(
      timestamp,
      CopyRecord(CastRecord(measurementRecord)),
      CastRecord(firstMeasurementRecord),
      CastRecord(lastMeasurementRecord)
    );
  }

  protected abstract TMeasurementRecord CorrectMeterId(
    TMeasurementRecord measurementRecord,
    string meterId
  );

  protected abstract TMeasurementRecord CorrectMeasurementLocationId(
    TMeasurementRecord measurementRecord,
    string measurementLocationId
  );

  protected abstract TMeasurementRecord CorrectTimestamp(
    TMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  );

  protected abstract TMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    TMeasurementRecord measurementRecord,
    TMeasurementRecord firstMeasurementRecord,
    TMeasurementRecord lastMeasurementRecord
  );

  protected abstract TMeasurementRecord CopyRecord(TMeasurementRecord record);

  protected decimal DiffMultiplier(
    DateTimeOffset timestamp,
    DateTimeOffset firstTimestamp,
    DateTimeOffset lastTimestamp
  )
  {
    var multiplier = (timestamp - firstTimestamp).Ticks /
      (lastTimestamp - firstTimestamp).Ticks;
    return multiplier;
  }

  private static TMeasurementRecord CastRecord(
    IMeasurementRecord measurementRecord)
  {
    return measurementRecord
        as TMeasurementRecord
      ?? throw new ArgumentException(
        $"Expected {typeof(TMeasurementRecord).Name}"
        + $", but got {measurementRecord.GetType().Name}",
        nameof(measurementRecord)
      );
  }
}
