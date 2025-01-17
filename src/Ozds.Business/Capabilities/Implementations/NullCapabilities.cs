using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Capabilities.Implementations;

public class NullCapabilities : ICapabilities
{
  public bool HasApparentPower { get; set; } = false;
}
