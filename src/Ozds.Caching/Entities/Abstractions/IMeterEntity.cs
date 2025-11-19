using Ozds.Caching.Entities.Complex;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities.Abstractions;

public interface IMeterEntity : ITrackableIdentifiableEntity
{
  public string MeasurementValidatorId { get; }

  public string? MessengerId { get; }

  public decimal ConnectionPower_W { get; }

  public HashSet<PhaseEntity> Phases { get; }

  public PeriodEntity MaxInactivityPeriod { get; }
}
