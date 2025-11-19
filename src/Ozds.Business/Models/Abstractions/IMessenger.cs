using Ozds.Business.Models.Complex;

namespace Ozds.Business.Models.Abstractions;

public interface IMessenger : ITrackableIdentifiable, ICachedIdentifiable
{
  string LocationId { get; set; }

  PeriodModel MaxInactivityPeriod { get; set; }

  PeriodModel PushDelayPeriod { get; set; }
}
