using Ozds.Business.Capabilities.Implementations.Abstractions;

namespace Ozds.Business.Models.Abstractions;

public interface IMeter : IAuditable
{
  public decimal ConnectionPower_W { get; }

  public string? MessengerId { get; }

  public ICapabilities Capabilities { get; }

  public HashSet<PhaseModel> Phases { get; }

  public string Kind { get; }
}
