using Ozds.Business.Math;
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
    var diffMultiplier = DiffMultiplier(
      timestamp,
      firstMeasurementRecord.Timestamp,
      lastMeasurementRecord.Timestamp
    );

    var activeEnergy = measurementRecord.ActiveEnergy_Wh +
      new MinMaxSpanningMeasure<float>(
        firstMeasurementRecord.ActiveEnergy_Wh,
        lastMeasurementRecord.ActiveEnergy_Wh
      ).SpanDiff * diffMultiplier;

    var reactiveEnergy = measurementRecord.ReactiveEnergy_VARh +
      new MinMaxSpanningMeasure<float>(
        firstMeasurementRecord.ReactiveEnergy_VARh,
        lastMeasurementRecord.ReactiveEnergy_VARh
      ).SpanDiff * diffMultiplier;

    var apparentEnergy = measurementRecord.ApparentEnergy_VAh +
      new MinMaxSpanningMeasure<float>(
        firstMeasurementRecord.ApparentEnergy_VAh,
        lastMeasurementRecord.ApparentEnergy_VAh
      ).SpanDiff * diffMultiplier;

    return measurementRecord;
  }
}
