using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Abstractions;

public interface IRecordCorrector
{
  bool CanCorrectFor(Type measurementRecordType);

  IMeasurementRecord CorrectMeterId(
    IMeasurementRecord measurementRecord,
    string meterId
  );

  IMeasurementRecord CorrectMeasurementLocationId(
    IMeasurementRecord measurementRecord,
    string measurementLocationId
  );

  IMeasurementRecord CorrectTimestamp(
    IMeasurementRecord measurementRecord,
    DateTimeOffset timestamp
  );

  IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  );
}
