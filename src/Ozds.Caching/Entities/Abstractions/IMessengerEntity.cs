using Ozds.Caching.Entities.Complex;

namespace Ozds.Caching.Entities.Abstractions;

public interface IMessengerEntity : ITrackableIdentifiableEntity
{
  string LocationId { get; set; }

  PeriodEntity MaxInactivityPeriod { get; set; }

  PeriodEntity PushDelayPeriod { get; set; }
}
