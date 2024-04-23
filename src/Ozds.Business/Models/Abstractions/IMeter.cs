using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public float ConnectionPower_W { get; }

  public string? MessengerId { get; }

  public string MeasurementValidatorId { get; }

  public ICapabilities Capabilities { get; }
}
