using Microsoft.AspNetCore.Components;
using Ozds.Business.Queries;
using Ozds.Client.State;

namespace Ozds.Client.Components.Base;

public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  [CascadingParameter]
  private TimeState TimeState { get; set; } = default!;

  private TimeQueries? timeQueries;

  private TimeQueries TimeQueries =>
    timeQueries ??= ScopedServices
      .GetRequiredService<TimeQueries>();

  protected TimeZoneInfo GetTimeZone()
  {
    if (TimeState is { } timeState)
    {
      return timeState.TimeZoneInfo;
    }

    return TimeQueries.DefaultTimeZone;
  }
}
