using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Capabilities.Implementations;

namespace Ozds.Business.Models;

public class AbbB2xMeterModel : MeterModel
{
  public override ICapabilities Capabilities
  {
    get { return new AbbB2xCapabilities(); }
  }
}
