using Ozds.Fake.Correction.Base;
using Ozds.Fake.Records;

namespace Ozds.Fake.Correction;

public class SchneideriEM3xxxMeasurementRecordCumulativeCorrector
  : CumulativeCorrector<SchneideriEM3xxxMeasurementRecord>
{
  protected override SchneideriEM3xxxMeasurementRecord CorrectCumulatives(
    DateTimeOffset timestamp,
    SchneideriEM3xxxMeasurementRecord measurementRecord,
    SchneideriEM3xxxMeasurementRecord firstMeasurementRecord,
    SchneideriEM3xxxMeasurementRecord lastMeasurementRecord
  )
  {
    return measurementRecord;
  }
}
