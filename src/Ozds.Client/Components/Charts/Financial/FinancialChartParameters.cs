using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Client.Components.Charts;

public class FinancialChartParameters
{
  public HashSet<IMeter> Meters { get; set; } = new();

  public HashSet<IMeasurementLocation> MeasurementLocations { get; set; } = new();

  public PaginatedList<IFinancial> Financials { get; set; } = new(new(), 0);

  public DateTimeOffset FromDate { get; set; } =
    DateTimeOffset.UtcNow.Subtract(TimeSpan.FromHours(1));

  public ResolutionModel Resolution { get; set; } = ResolutionModel.Hour;

  public int Multiplier { get; set; } = 1;

  public OperatorModel Operator { get; set; } = OperatorModel.Sum;
}
