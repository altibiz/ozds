using Ozds.Business.Capabilities;
using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class AbbB2xMeterModel : MeterModel
{
  public override ICapabilities Capabilities
  {
    get { return new AbbB2xCapabilities(); }
  }
}
