using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Abstractions;

public interface ICumulativeCorrector
{
  bool CanCorrectCumulativesFor(Type measurementRecordType);

  IMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  );
}
