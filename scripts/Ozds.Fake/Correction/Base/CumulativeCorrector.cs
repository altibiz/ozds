using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Base;

public abstract class
  CumulativeCorrector<TMeasurementRecord> : ICumulativeCorrector
  where TMeasurementRecord : class, IMeasurementRecord
{
  public bool CanCorrectCumulativesFor(Type measurementRecordType)
  {
    return measurementRecordType.IsAssignableFrom(typeof(TMeasurementRecord));
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
      CopyRecord(
        measurementRecord as TMeasurementRecord
        ?? throw new ArgumentException(
          $"Expected {
            typeof(TMeasurementRecord).Name
          }, but got {
            measurementRecord.GetType().Name
          }",
          nameof(measurementRecord)
        )),
      firstMeasurementRecord
        as TMeasurementRecord
      ?? throw new ArgumentException(
        $"Expected {
          typeof(TMeasurementRecord).Name
        }, but got {
          firstMeasurementRecord.GetType().Name
        }",
        nameof(firstMeasurementRecord)
      ),
      lastMeasurementRecord
        as TMeasurementRecord
      ?? throw new ArgumentException(
        $"Expected {
          typeof(TMeasurementRecord).Name
        }, but got {
          lastMeasurementRecord.GetType().Name
        }",
        nameof(lastMeasurementRecord)
      )
    );
  }

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
}
