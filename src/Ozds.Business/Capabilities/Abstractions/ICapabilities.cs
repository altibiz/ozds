using Ozds.Business.Models.Enums;

namespace Ozds.Business.Capabilities.Abstractions;

public interface ICapabilities
{
  public IEnumerable<MeasureModel> Measures { get; }
}
