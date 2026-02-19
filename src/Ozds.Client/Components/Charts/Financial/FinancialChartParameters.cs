using System.Globalization;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries.Abstractions;

namespace Ozds.Client.Components.Charts;

public class FinancialChartParameters
{
  public HashSet<IMeter> Meters { get; set; } = new();

  public HashSet<IMeasurementLocation> MeasurementLocations { get; set; } =
    new();

  public PaginatedList<IFinancial> Financials { get; set; } =
    new(new List<IFinancial>(), 0);

  public DateTimeOffset FromDate { get; set; } =
    // NOTE: just so something is there
    DateTimeOffset.Parse("2000-01-01T00:00:00Z", CultureInfo.InvariantCulture);

  public ResolutionModel Resolution { get; set; } = ResolutionModel.Minute;

  public int Multiplier { get; set; } = 15;

  public OperatorModel Operator { get; set; } = OperatorModel.Sum;
}
