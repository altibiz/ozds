using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public decimal ConnectionPower_W { get; }

  public string? MessengerId { get; }

  public string MeasurementValidatorId { get; }

  public ICapabilities Capabilities { get; }

  public HashSet<PhaseModel> Phases { get; }
}
