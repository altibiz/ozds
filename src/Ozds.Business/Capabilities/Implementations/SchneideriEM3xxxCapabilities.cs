using Ozds.Business.Capabilities.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Business.Capabilities.Implementations;

public class SchneideriEM3xxxCapabilities : ICapabilities
{
  public IEnumerable<MeasureModel> Measures { get; } = new[]
  {
    MeasureModel.Current,
    MeasureModel.Voltage,
    MeasureModel.ActivePower,
    MeasureModel.ReactivePower,
    MeasureModel.ActiveEnergy,
    MeasureModel.ReactiveEnergy
  };
}
