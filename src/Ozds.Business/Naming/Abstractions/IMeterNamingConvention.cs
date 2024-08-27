namespace Ozds.Business.Naming.Abstractions;

public interface IMeterNamingConvention
{
  public string AbbIdPrefix { get; }
  public string SchneiderIdPrefix { get; }

  public Type AbbB2xMeasurementType { get; }
  public Type SchneideriEM3xxxMeasurementType { get; }

  public Type AbbB2xAggregateType { get; }
  public Type SchneideriEM3xxxAggregateType { get; }

  public Type AbbB2xMeasurementValidatorType { get; }
  public Type SchneideriEM3xxxMeasurementValidatorType { get; }

  public Type AbbB2xMeterType { get; }
  public Type SchneideriEM3xxxMeterType { get; }
}
