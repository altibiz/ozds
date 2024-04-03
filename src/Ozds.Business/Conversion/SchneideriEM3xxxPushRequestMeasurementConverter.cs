using Ozds.Business.Conversion.Base;
using Ozds.Business.Iot;
using Ozds.Business.Models;

namespace Ozds.Business.Conversion;

public class SchneideriEM3xxxPushRequestMeasurementConverter : PushRequestMeasurementConverter<SchneideriEM3xxxPushRequest, SchneideriEM3xxxMeasurementModel>
{
  protected override string MeterIdPrefix => "schneider-iEM3xxx";

  protected override SchneideriEM3xxxMeasurementModel ToMeasurement(SchneideriEM3xxxPushRequest pushRequest, string meterId, DateTimeOffset timestamp) =>
    pushRequest.ToModel(meterId, timestamp);
}
