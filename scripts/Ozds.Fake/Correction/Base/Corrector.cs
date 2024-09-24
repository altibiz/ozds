using Ozds.Fake.Correction.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Base;

public abstract class
  Corrector<TMeasurementRecord> : ICorrector
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
      measurementRecord
        as TMeasurementRecord
      ?? throw new ArgumentException(
        $"Expected {
          typeof(TMeasurementRecord).Name
        }, but got {
          measurementRecord.GetType().Name
        }",
        nameof(measurementRecord)
      ),
      meterId
    );
  }

  public IMeasurementRecord CorrectTimestamp(
    IMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  )
  {
    return CorrectTimestamp(
      measurementRecord
        as TMeasurementRecord
      ?? throw new ArgumentException(
        $"Expected {
          typeof(TMeasurementRecord).Name
        }, but got {
          measurementRecord.GetType().Name
        }",
        nameof(measurementRecord)
      ),
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

  protected abstract TMeasurementRecord CorrectMeterId(
    TMeasurementRecord measurementRecord,
    string meterId
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
}
