using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities;

public class MeasurementScopeEntity : ScopeEntity
{
  public IntervalEntity Interval { get; set; } = default!;
}
