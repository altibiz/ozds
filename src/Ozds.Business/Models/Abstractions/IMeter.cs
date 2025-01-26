using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public string? MessengerId { get; }

  public decimal ConnectionPower_W { get; }

  public ICapabilities Capabilities { get; }

  public HashSet<PhaseModel> Phases { get; }
}
