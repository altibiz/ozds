using System.Globalization;
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
    // NOTE: just so something is there
    DateTimeOffset.Parse(
      "2000-01-01T00:00:00Z",
      CultureInfo.InvariantCulture);

  public OperatorModel Operator { get; set; } = OperatorModel.Last;
}
