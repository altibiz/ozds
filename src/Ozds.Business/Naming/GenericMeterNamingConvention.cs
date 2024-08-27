using Ozds.Business.Models;
using Ozds.Business.Naming.Base;

namespace Ozds.Business.Naming;

public class GenericMeterNamingConvention : MeterNamingConvention
{
  public override string AbbIdPrefix => "abb-b2x";
  public override string SchneiderIdPrefix => "schneider-iem3xxx";

  public override Type AbbB2xMeasurementType => typeof(AbbB2xMeasurementModel);
  public override Type SchneideriEM3xxxMeasurementType => typeof(SchneideriEM3xxxMeasurementModel);

  public override Type AbbB2xAggregateType => typeof(AbbB2xAggregateModel);
  public override Type SchneideriEM3xxxAggregateType => typeof(SchneideriEM3xxxAggregateModel);

  public override Type AbbB2xMeasurementValidatorType => typeof(AbbB2xMeasurementValidatorModel);
  public override Type SchneideriEM3xxxMeasurementValidatorType => typeof(SchneideriEM3xxxMeasurementValidatorModel);

  public override Type AbbB2xMeterType => typeof(AbbB2xMeterModel);
  public override Type SchneideriEM3xxxMeterType => typeof(SchneideriEM3xxxMeterModel);
}
