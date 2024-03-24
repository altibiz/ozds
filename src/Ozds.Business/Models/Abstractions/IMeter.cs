using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public string? NetworkUserMeasurementLocationId { get; }

  public string? LocationMeasurementLocationId { get; }

  public ICapabilities Capabilities { get; }
}
