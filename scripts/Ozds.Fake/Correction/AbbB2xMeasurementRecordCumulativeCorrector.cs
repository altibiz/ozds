using Ozds.Fake.Correction.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Correction;

public class AbbB2xMeasurementRecordCumulativeCorrector
  : CumulativeCorrector<AbbB2xMeasurementRecord>
{
  protected override AbbB2xMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    AbbB2xMeasurementRecord measurementRecord,
    AbbB2xMeasurementRecord firstMeasurementRecord,
    AbbB2xMeasurementRecord lastMeasurementRecord
  )
  {
    return measurementRecord;
  }
}
