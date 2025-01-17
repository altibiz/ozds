using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Capabilities.Implementations;

public class SchneideriEM3xxxCapabilities : ICapabilities
{
  public bool HasApparentPower { get; set; } = true;
}
