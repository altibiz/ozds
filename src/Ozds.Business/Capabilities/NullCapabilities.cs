using Ozds.Business.Capabilities.Abstractions;

namespace Ozds.Business.Capabilities;

public class NullCapabilities : ICapabilities
{
  public bool hasApparentPower { get; set; } = false;
}
