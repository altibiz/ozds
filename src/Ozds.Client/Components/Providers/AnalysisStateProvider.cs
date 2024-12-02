using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class AnalysisStateProvider : OzdsOwningComponentBase
{
  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private LocationState LocationState { get; set; } = default!;

  private string? _previousRepresentativeId;

  private string? _previousLocationId;

  private List<AnalysisBasisModel>? _analysisBases;

  private AnalysisState? _state;

  protected override void OnParametersSet()
  {
    _state = new AnalysisState(
      new(() =>
      {
        InvokeAsync(async () =>
        {
          if (_previousRepresentativeId == RepresentativeState.Representative.Id
          && _previousLocationId == LocationState.Location?.Id)
          {
            return;
          }
          _previousRepresentativeId = RepresentativeState.Representative.Id;
          _previousLocationId = LocationState.Location?.Id;

          var analysisQueries = ScopedServices
            .GetRequiredService<AnalysisQueries>();

          var now = DateTimeOffset.UtcNow;
          var aYearAgo = now.AddYears(-1);

          var analysisBases = await analysisQueries
            .ReadAnalysisBasesByRepresentative(
              RepresentativeState.Representative.Id,
              RepresentativeState.Representative.Role,
              aYearAgo,
              now,
              CancellationToken.None,
              LocationState.Location?.Id
            );

          _analysisBases = analysisBases;

          StateHasChanged();
        });

        return _analysisBases ?? new();
      })
    );
  }
}
