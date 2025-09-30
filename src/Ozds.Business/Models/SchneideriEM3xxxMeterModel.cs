using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Capabilities.Implementations;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeterModel : MeterModel
{
  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
  }
}
