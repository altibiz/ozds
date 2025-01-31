using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Capabilities.Implementations;
using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming.Implementations;

public class
  SchneideriEM3xxxMeterNamingConvention : ConcreteMeterNamingConvention
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

  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
  }
}
