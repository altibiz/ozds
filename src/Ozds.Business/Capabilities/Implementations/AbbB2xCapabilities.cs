using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Capabilities.Implementations;

public class AbbB2xCapabilities : ICapabilities
{
  public IEnumerable<MeasureModel> Measures { get; } = new[]
  {
    MeasureModel.Current,
    MeasureModel.Voltage,
    MeasureModel.ActivePower,
    MeasureModel.ReactivePower,
    MeasureModel.ApparentPower,
    MeasureModel.ActiveEnergy,
    MeasureModel.ReactiveEnergy,
    MeasureModel.ApparentEnergy
  };
}
