using Microsoft.AspNetCore.Components;
using Ozds.Business.Analysis;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Enums;
using Ozds.Business.Queries;
using Ozds.Business.Time;
using Ozds.Client.Components.Models.Base;
using Ozds.Client.State;

namespace Ozds.Client.Pages;

public partial class NetworkUserPage
  : OzdsIdentifiableModelPageComponentBase<NetworkUserModel>
{
  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  private IEnumerable<IAggregate> measurements = new List<IAggregate>();

  private List<MeterAnalysis> analysis = new();

  private DateTime? selectedMonth;

  private async Task<NetworkUserModel?> OnLoadAsync()
  {
    if (Id is null)
    {
      return null;
    }

    var queries = ScopedServices.GetRequiredService<NetworkUserQueries>();

    var networkUser = await queries.ReadNetworkUserByRepresentativeId(
      RepresentativeState.Representative.Id,
      RepresentativeState.Representative.Role,
      Id,
      CancellationToken
    );

    return networkUser;
  }

  protected override async Task OnParametersSetAsync()
  {
    analysis = AnalysisState
      .AnalysisBases.Value.AnalysesByMeter()
      .Where(x => x.NetworkUser?.Id == Id)
      .ToList();

    if (measurements.Any())
    {
      await OnDateChanged(selectedMonth);
    }
  }

  private async Task OnDateChanged(DateTime? date)
  {
    selectedMonth = date;
    if (analysis is not null)
    {
      var meters = analysis.Select(x => (IMeter)x!.Meter).ToList();
      DateTimeOffset dto = new DateTimeOffset(
        selectedMonth!.Value,
        TimeSpan.Zero
      );
      var queries = ScopedServices.GetRequiredService<MeasurementQueries>();
      var measures = await queries.ReadByMeterIdsDynamic(
        meters,
        ResolutionModel.Year,
        30,
        0,
        CancellationToken,
        dto.GetStartOfMonth(),
        dto.GetStartOfNextMonth()
      );
      var orderedMeasures = measures.Items.OrderBy(x => x.Timestamp).ToList();
      measurements = orderedMeasures.Select(x => (IAggregate)x).ToList();
    }
  }
}
