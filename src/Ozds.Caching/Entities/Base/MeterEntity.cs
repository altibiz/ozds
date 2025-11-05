using Ozds.Caching.Entities.Abstractions;
using Ozds.Caching.Entities.Complex;
using Ozds.Caching.Entities.Enums;

namespace Ozds.Caching.Entities.Base;

public abstract class MeterEntity : TrackableEntity, IMeterEntity
{
  public string MeasurementValidatorId { get; set; } = default!;

  public required string? MessengerId { get; set; }

  public decimal ConnectionPower_W { get; set; } = default!;

  public required HashSet<PhaseEntity> Phases { get; set; } = [];

  public PeriodEntity MaxInactivityPeriod { get; set; } = default!;
}
