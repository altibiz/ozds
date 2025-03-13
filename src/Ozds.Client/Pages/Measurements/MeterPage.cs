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

public partial class MeterPage : OzdsIdentifiableModelPageComponentBase<IMeter>
{
  private MeterAnalysis? analysis;

  private IEnumerable<object> measurements = new List<object>();

  private List<IMeter> meters = new();

  private DateTime? selectedMonth;

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  [Parameter]
  public string Id { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private Breakpoint Breakpoint { get; set; }

  private async Task OnDateChanged(DateTime? date)
  {
    selectedMonth = date;
    if (analysis is not null)
    {
      meters = new List<IMeter> { analysis.Meter };
      var dto = new DateTimeOffset(
        selectedMonth!.Value,
        TimeSpan.Zero
      );
      var queries = ScopedServices.GetRequiredService<MeasurementQueries>();
      var measures = await queries.ReadByMeterIdsDynamic(
        meters,
        ResolutionModel.Hour,
        30,
        0,
        CancellationToken,
        dto.GetStartOfMonth(),
        dto.GetStartOfNextMonth(),
        5000
      );
      measurements = measures.Items.OrderBy(x => x.Timestamp).ToList();
    }
  }

  protected override async Task OnParametersSetAsync()
  {
    analysis = AnalysisState
      .AnalysisBases.Value.AnalysesByMeter()
      .FirstOrDefault(x => x.Meter.Id == Id);

    if (measurements.Any())
    {
      await OnDateChanged(selectedMonth);
    }
  }
}
