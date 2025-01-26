using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Capabilities.Implementations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class SchneideriEM3xxxMeterModel : MeterModel
{
  public override ICapabilities Capabilities
  {
    get { return new SchneideriEM3xxxCapabilities(); }
  }
}
