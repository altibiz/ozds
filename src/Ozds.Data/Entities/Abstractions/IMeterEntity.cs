using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Abstractions;

public interface IMeterEntity : IAuditableEntity
{
  public string? MessengerId { get; }

  public float ConnectionPower_W { get; }

  public List<PhaseEntity> Phases { get; }

  public string MeasurementValidatorId { get; }
}
