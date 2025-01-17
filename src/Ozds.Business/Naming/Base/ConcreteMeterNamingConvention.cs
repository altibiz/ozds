using Ozds.Business.Naming.Abstractions;

namespace Ozds.Business.Naming.Base;

#pragma warning disable S1694 // An abstract class should have both abstract and concrete methods
public abstract class ConcreteMeterNamingConvention : IMeterNamingConvention
#pragma warning restore S1694 // An abstract class should have both abstract and concrete methods
{
  public abstract string IdPrefix { get; }
  public abstract Type MeasurementType { get; }
  public abstract Type AggregateType { get; }
  public abstract Type MeasurementValidatorType { get; }
  public abstract Type MeterType { get; }
}
