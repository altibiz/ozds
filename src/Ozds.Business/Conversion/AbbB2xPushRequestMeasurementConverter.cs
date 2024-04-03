using Ozds.Business.Conversion.Base;
using Ozds.Business.Iot;
using Ozds.Business.Models;

namespace Ozds.Business.Conversion;

public class AbbB2xPushRequestConverter : PushRequestMeasurementConverter<AbbB2xPushRequest, AbbB2xMeasurementModel>
{
  protected override string MeterIdPrefix => "abb-B2x";

  protected override AbbB2xMeasurementModel ToMeasurement(AbbB2xPushRequest pushRequest, string meterId, DateTimeOffset timestamp) =>
    pushRequest.ToModel(meterId, timestamp);
}
