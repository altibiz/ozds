using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Complex;

namespace Ozds.Caching.Entities.Base;

public abstract class MessengerEntity : TrackableEntity, IMessengerEntity
{
  public string LocationId { get; set; } = default!;

  public PeriodEntity MaxInactivityPeriod { get; set; } = default!;

  public PeriodEntity PushDelayPeriod { get; set; } = default!;
}
