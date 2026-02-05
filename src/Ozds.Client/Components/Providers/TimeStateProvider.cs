using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Ozds.Business.Queries;
using Ozds.Client.Components.Base;
using Ozds.Client.State;

namespace Ozds.Client.Components.Providers;

// NOTE: to be used inside ErrorBoundary and ScopeStateProvider
public partial class TimeStateProvider : DisposableComponentBase
{
  private TimeState? _state;

  private TimeQueries? timeQueries;

  [CascadingParameter]
  private ScopeState ScopeState { get; set; } = default!;

  [Parameter]
  public RenderFragment? ChildContent { get; set; }

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  protected IServiceProvider ScopedServices
  {
    get
    {
      return ScopeState?.ScopedServices
        ?? throw new InvalidOperationException($"{this} got disposed");
    }
  }

  private TimeQueries TimeQueries
  {
    get
    {
      return timeQueries ??= ScopedServices.GetRequiredService<TimeQueries>();
    }
  }

  protected override async Task OnParametersSetAsync()
  {
    var timeZone = await GetTimeZoneFromBrowser();
    if (timeZone is null)
    {
      timeZone = TimeQueries.DefaultTimeZone;
    }

    _state = new TimeState(timeZone);
  }

  private async Task<TimeZoneInfo?> GetTimeZoneFromBrowser()
  {
    try
    {
      var id = await JS.InvokeAsync<string>("window.ozdsGetTimeZone");
      return id is null ? default : TimeQueries.IdToTimeZone(id);
    }
    catch (Exception)
    {
      return default;
    }
  }
}
