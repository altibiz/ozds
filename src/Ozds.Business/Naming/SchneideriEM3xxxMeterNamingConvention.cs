using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming;

public class SchneideriEM3xxxMeterNamingConvention : MeterNamingConvention
{
  public override string IdPrefix
  {
    get { return "schneider-iEM3xxx"; }
  }

  public override Type MeasurementType
  {
    get { return typeof(SchneideriEM3xxxMeasurementModel); }
  }

  public override Type AggregateType
  {
    get { return typeof(SchneideriEM3xxxAggregateModel); }
  }

  public override Type MeasurementValidatorType
  {
    get { return typeof(SchneideriEM3xxxMeasurementValidatorModel); }
  }

  public override Type MeterType
  {
    get { return typeof(SchneideriEM3xxxMeterModel); }
  }
}
