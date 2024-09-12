using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Abstractions;

public interface ICorrector
{
  bool CanCorrectFor(Type measurementRecordType);

  IMeasurementRecord CorrectMeterId(
    IMeasurementRecord measurementRecord,
    string meterId
  );

  IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  );
}
