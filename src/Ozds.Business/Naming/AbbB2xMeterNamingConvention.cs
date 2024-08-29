using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming;

public class AbbB2xMeterNamingConvention : MeterNamingConvention
{
  public override string IdPrefix
  {
    get { return "abb-b2x"; }
  }

  public override Type MeasurementType
  {
    get { return typeof(AbbB2xMeasurementModel); }
  }

  public override Type AggregateType
  {
    get { return typeof(AbbB2xAggregateModel); }
  }

  public override Type MeasurementValidatorType
  {
    get { return typeof(AbbB2xMeasurementValidatorModel); }
  }

  public override Type MeterType
  {
    get { return typeof(AbbB2xMeterModel); }
  }
}
