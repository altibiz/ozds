using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Base;

public abstract class
  ConcreteRecordCorrector<TMeasurementRecord,
    TMeasurementValidator> : IRecordCorrector
  where TMeasurementRecord : class, IMeasurementRecord
  where TMeasurementValidator : class, IMeasurementValidator
{
  public bool CanCorrectFor(Type measurementRecordType)
  {
    return measurementRecordType.IsAssignableFrom(typeof(TMeasurementRecord));
  }

  public IMeasurementRecord CopyRecord(IMeasurementRecord record)
  {
    return CopyRecord(CastRecord(record));
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
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  )
  {
    return CorrectCumulatives(
      CastRecord(measurementRecord),
      CastRecord(firstMeasurementRecord),
      CastRecord(lastMeasurementRecord)
    );
  }

  public IMeasurementRecord CorrectValidation(
    IMeasurementRecord measurementRecord,
    IMeasurementValidator validator
  )
  {
    return CorrectValidation(
      CastRecord(measurementRecord),
      CastValidator(validator)
    );
  }

  protected abstract TMeasurementRecord CopyRecord(TMeasurementRecord record);

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
    TMeasurementRecord measurementRecord,
    TMeasurementRecord firstMeasurementRecord,
    TMeasurementRecord lastMeasurementRecord
  );

  protected abstract TMeasurementRecord CorrectValidation(
    TMeasurementRecord measurementRecord,
    TMeasurementValidator validator
  );

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

  protected static decimal Clamp(
    decimal value,
    decimal min,
    decimal max
  )
  {
    var ceiledMin = Math.Ceiling(min * BaseCorrectorConstants.EpsilonMultiplier)
      / BaseCorrectorConstants.EpsilonMultiplier;
    var flooredMax = Math.Floor(max * BaseCorrectorConstants.EpsilonMultiplier)
      / BaseCorrectorConstants.EpsilonMultiplier;
    var clamped = Math.Clamp(
      value,
      ceiledMin + BaseCorrectorConstants.EpsilonValue,
      flooredMax - BaseCorrectorConstants.EpsilonValue);
    return clamped;
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

  private static TMeasurementValidator CastValidator(
    IMeasurementValidator validator)
  {
    return validator
        as TMeasurementValidator
      ?? throw new ArgumentException(
        $"Expected {typeof(TMeasurementValidator).Name}"
        + $", but got {validator.GetType().Name}",
        nameof(validator)
      );
  }
}
