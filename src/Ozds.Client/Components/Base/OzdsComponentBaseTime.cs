using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.State;

namespace Ozds.Client.Components.Base;

public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  private TimeQueries? timeQueries;

  [CascadingParameter]
  private TimeState TimeState { get; set; } = default!;

  private TimeQueries TimeQueries
  {
    get
    {
      return timeQueries ??= ScopedServices
        .GetRequiredService<TimeQueries>();
    }
  }

  protected TimeZoneInfo GetTimeZone()
  {
    if (TimeState is { } timeState)
    {
      return timeState.TimeZoneInfo;
    }

    return TimeQueries.DefaultTimeZone;
  }
}
