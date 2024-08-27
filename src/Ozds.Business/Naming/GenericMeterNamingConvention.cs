using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming;

public class GenericMeterNamingConvention : MeterNamingConvention
{
  public override string AbbIdPrefix
  {
    get { return "abb-b2x"; }
  }

  public override string SchneiderIdPrefix
  {
    get { return "schneider-iem3xxx"; }
  }

  public override Type AbbB2xMeasurementType
  {
    get { return typeof(AbbB2xMeasurementModel); }
  }

  public override Type SchneideriEM3xxxMeasurementType
  {
    get { return typeof(SchneideriEM3xxxMeasurementModel); }
  }

  public override Type AbbB2xAggregateType
  {
    get { return typeof(AbbB2xAggregateModel); }
  }

  public override Type SchneideriEM3xxxAggregateType
  {
    get { return typeof(SchneideriEM3xxxAggregateModel); }
  }

  public override Type AbbB2xMeasurementValidatorType
  {
    get { return typeof(AbbB2xMeasurementValidatorModel); }
  }

  public override Type SchneideriEM3xxxMeasurementValidatorType
  {
    get { return typeof(SchneideriEM3xxxMeasurementValidatorModel); }
  }

  public override Type AbbB2xMeterType
  {
    get { return typeof(AbbB2xMeterModel); }
  }

  public override Type SchneideriEM3xxxMeterType
  {
    get { return typeof(SchneideriEM3xxxMeterModel); }
  }
}
