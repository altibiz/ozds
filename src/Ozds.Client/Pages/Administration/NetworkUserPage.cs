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
  private List<MeasurementLocationAnalysis> analysis = new();

  private DateTime? selectedMonth;

  [Parameter]
  public string? Id { get; set; }

  [CascadingParameter]
  private AnalysisState AnalysisState { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

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

  protected override void OnParametersSet()
  {
    analysis = AnalysisState
      .AnalysisBases.Value.AnalysesByMeasurementLocation()
      .Where(x => x.NetworkUser?.Id == Id)
      .ToList();
  }

  private void OnDateChanged(DateTime? date)
  {
    selectedMonth = date;
  }
}
