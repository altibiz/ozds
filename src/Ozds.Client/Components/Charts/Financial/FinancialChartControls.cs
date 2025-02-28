using Microsoft.AspNetCore.Components;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Queries.Abstractions;
using Ozds.Client.Components.Base;

namespace Ozds.Client.Components.Charts;

public partial class FinancialChartControls : OzdsComponentBase
{
  private readonly FinancialChartParameters _parameters = new();

  [Parameter]
  public List<IMeasurementLocation> MeasurementLocations { get; set; } =
    default!;

  [Parameter]
  public List<IMeter> Meters { get; set; } = default!;

  [Parameter]
  public HashSet<FinancialChartProfile> Profiles { get; set; } = new();

  [Parameter]
  public RenderFragment<FinancialChartParameters> ChildContent { get; set; } =
    default!;

  protected override async Task OnParametersSetAsync()
  {
    var queries = ScopedServices.GetRequiredService<FinancialQueries>();

    var fromDate = _parameters.FromDate;
    var toDate = fromDate
      .Add(
        _parameters.Resolution
          .ToTimeSpan(_parameters.Multiplier, fromDate));

    var fromMeters = await queries.ReadByMeterIds(
      Meters.Select(meter => meter.Id),
      _parameters.Resolution,
      _parameters.Multiplier,
      1,
      CancellationToken,
      fromDate,
      toDate
    );

    var fromMeasurementLocations = await queries.ReadByMeasurementLocationIds(
      MeasurementLocations.Select(
        measurementLocation => measurementLocation.Id),
      _parameters.Resolution,
      _parameters.Multiplier,
      1,
      CancellationToken,
      fromDate,
      toDate
    );

    _parameters.Financials = new PaginatedList<IFinancial>(
      fromMeters.Items
        .Concat(fromMeasurementLocations.Items)
        .DistinctBy(
          financial => new
          {
            Type = financial.GetType(),
            financial.Id
          })
        .ToList(),
      fromMeters.TotalCount + fromMeasurementLocations.TotalCount
    );
  }
}
