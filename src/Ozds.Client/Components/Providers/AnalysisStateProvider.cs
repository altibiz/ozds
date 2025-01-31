using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ozds.Business.Models.Composite;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

public partial class AnalysisStateProvider : OzdsComponentBase
{
  private string? _previousLocationId;

  private string? _previousRepresentativeId;

  private AnalysisState? _state;

  [Parameter]
  public RenderFragment ChildContent { get; set; } = default!;

  [CascadingParameter]
  private RepresentativeState RepresentativeState { get; set; } = default!;

  [CascadingParameter]
  private LocationState LocationState { get; set; } = default!;

  [Inject]
  private ILogger<AnalysisStateProvider> Logger { get; set; } = default!;

  protected override void OnParametersSet()
  {
    if (_previousRepresentativeId == RepresentativeState.Representative.Id
      && _previousLocationId == LocationState.Location?.Id)
    {
      return;
    }

    _previousRepresentativeId = RepresentativeState.Representative.Id;
    _previousLocationId = LocationState.Location?.Id;

    _state = new AnalysisState(
      new Lazy<List<AnalysisBasisModel>>(
        () =>
        {
          Task.Run(
            async () =>
            {
              try
              {
                var analysisQueries = ScopedServices
                  .GetRequiredService<AnalysisQueries>();

                var now = DateTimeOffset.UtcNow;
                var aYearAgo = now.AddYears(-1);

                var analysisBases = await analysisQueries
                  .ReadAnalysisBasesByRepresentativeAndLocation(
                    RepresentativeState.Representative.Id,
                    RepresentativeState.Representative.Role,
                    aYearAgo,
                    now,
                    LocationState.Location?.Id,
                    CancellationToken
                  );

                _state = new AnalysisState(
                  new Lazy<List<AnalysisBasisModel>>(() => analysisBases));

                await InvokeAsync(StateHasChanged);
              }
              catch (Exception ex)
              {
                Logger.LogError(ex, "Error setting analysis bases");
              }
            });

          return new List<AnalysisBasisModel>();
        })
    );
  }
}
