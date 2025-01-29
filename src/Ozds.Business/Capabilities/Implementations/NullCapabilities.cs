using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Capabilities.Implementations;

public class NullCapabilities : ICapabilities
{
  public IEnumerable<MeasureModel> Measures => [];
}
