using Microsoft.AspNetCore.Components;
using MudBlazor;
using Ozds.Business.Analysis;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class MeasurementLocationPage
  : OzdsIdentifiableModelPageComponentBase<IMeasurementLocation>
{
  private MeasurementLocationAnalysis? analysis;

  private List<IMeasurementLocation> measurementLocations = new();

  private IEnumerable<IAggregate> measurements = new List<IAggregate>();

  private DateTime? selectedMonth;

  [CascadingParameter]
  public AnalysisState AnalysisState { get; set; } = default!;

  [CascadingParameter]
  public Breakpoint Breakpoint { get; set; }

  [Parameter]
  public string Id { get; set; } = default!;

  private async Task OnDateChanged(DateTime? date)
  {
    selectedMonth = date;
    if (analysis is not null)
    {
      measurementLocations = new List<IMeasurementLocation>
      {
        analysis.MeasurementLocation
      };
      var dto = new DateTimeOffset(
        selectedMonth!.Value,
        TimeSpan.Zero
      );
      var queries = ScopedServices.GetRequiredService<MeasurementQueries>();
      var measures = await queries.ReadByMeasurementLocationIdsDynamic(
        measurementLocations,
        ResolutionModel.Hour,
        30,
        0,
        CancellationToken,
        dto.GetStartOfMonth(),
        dto.GetStartOfNextMonth(),
        5000
      );
      var orderedMeasures = measures.Items.OrderBy(x => x.Timestamp).ToList();
      measurements = orderedMeasures.Select(x => (IAggregate)x).ToList();
    }
  }

  protected override async Task OnParametersSetAsync()
  {
    analysis = AnalysisState
      .AnalysisBases.Value.AnalysesByMeasurementLocation()
      .FirstOrDefault(x => x.MeasurementLocation.Id == Id);

    if (measurements.Any())
    {
      await OnDateChanged(selectedMonth);
    }
  }
}
