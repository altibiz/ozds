using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Client.Components.Charts;

public class MeasurementChartParameters
{
  public HashSet<IMeter> Meters { get; set; } = new();

  public HashSet<IMeasurementLocation> MeasurementLocations { get; set; } =
    new();

  public PaginatedList<IMeasurement> Measurements { get; set; } =
    new(new List<IMeasurement>(), 0);

  public MeasureModel Measure { get; set; } = MeasureModel.ActivePower;

  public HashSet<PhaseModel> Phases { get; set; } = new();

  public ResolutionModel Resolution { get; set; } = ResolutionModel.Minute;

  public int Multiplier { get; set; } = 15;

  public bool Refresh { get; set; } = true;

  public DateTimeOffset FromDate { get; set; } =
    DateTimeOffset.UtcNow.Subtract(
      ResolutionModel.Minute.ToTimeSpan(15, DateTimeOffset.UtcNow)
    );

  public OperatorModel Operator { get; set; } = OperatorModel.Last;
}
