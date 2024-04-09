using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public string? MessengerId { get; }

  public string MeasurementValidatorId { get; }

  public ICapabilities Capabilities { get; }
}
