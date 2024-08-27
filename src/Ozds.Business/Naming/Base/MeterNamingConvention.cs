using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Base;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods
public abstract class MeterNamingConvention : IMeterNamingConvention
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
{
  public abstract string AbbIdPrefix { get; }
  public abstract string SchneiderIdPrefix { get; }

  public abstract Type AbbB2xMeasurementType { get; }
  public abstract Type SchneideriEM3xxxMeasurementType { get; }

  public abstract Type AbbB2xAggregateType { get; }
  public abstract Type SchneideriEM3xxxAggregateType { get; }

  public abstract Type AbbB2xMeasurementValidatorType { get; }
  public abstract Type SchneideriEM3xxxMeasurementValidatorType { get; }

  public abstract Type AbbB2xMeterType { get; }
  public abstract Type SchneideriEM3xxxMeterType { get; }
}
