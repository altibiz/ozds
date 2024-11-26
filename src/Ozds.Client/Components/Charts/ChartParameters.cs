using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;

namespace Ozds.Client.Components.Charts;

public class ChartParameters
{
  public HashSet<IMeter> Selection { get; set; } = new();

  public MeasureModel Measure { get; set; } = MeasureModel.ActivePower;

  public HashSet<PhaseModel> Phases { get; set; } = new();

  public DateTimeOffset StartDate { get; set; } = default;

  public ResolutionModel Resolution { get; set; } = ResolutionModel.Hour;

  public int Multiplier { get; set; } = 1;

  public bool Refresh { get; set; } = true;

  public OperatorModel Operator { get; set; } = OperatorModel.Sum;
}
