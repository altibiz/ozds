using Ozds.Business.Models.Abstractions;
using Ozds.Fake.Records.Abstractions;

namespace Ozds.Fake.Correction.Abstractions;

public interface IRecordCorrector
{
  bool CanCorrectFor(Type measurementRecordType);

  IMeasurementRecord CopyRecord(IMeasurementRecord record);

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
    IMeasurementRecord measurementRecord,
    IMeasurementRecord firstMeasurementRecord,
    IMeasurementRecord lastMeasurementRecord
  );

  IMeasurementRecord CorrectValidation(
    IMeasurementRecord measurementRecord,
    IMeasurementValidator validator
  );
}
