using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Naming.Abstractions;

public interface IMeterNamingConvention
{
  public string IdPrefix { get; }
  public Type MeasurementType { get; }
  public Type AggregateType { get; }
  public Type MeasurementValidatorType { get; }
  public Type MeterType { get; }
  public ICapabilities Capabilities { get; }
}
