using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming;

public class EgaugeLineNamingConvention : LineNamingConvention
{
  public override string LineIdPrefix => "e";

  public override string MeterIdPrefix => "egauge";

  public override Type LineType => typeof(EgaugeLineModel);

  public override Type MeasurementType => typeof(EgaugeMeasurementModel);

  public override Type AggregateType => typeof(EgaugeAggregateModel);

  public override Type MeasurementValidatorType => typeof(EgaugeMeasurementValidatorModel);

  public override Type MeterType => typeof(EgaugeMeterModel);
}
