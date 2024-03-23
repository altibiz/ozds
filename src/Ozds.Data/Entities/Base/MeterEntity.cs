using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities.Base;

[Table("meters")]
public abstract class MeterEntity : SoftDeletableEntity
{
  public virtual NetworkUserMeasurementLocationEntity? NetworkUserMeasurementLocation { get; set; } = default!;

  public virtual LocationMeasurementLocationEntity? LocationMeasurementLocation { get; set; } = default!;

  public virtual List<MeterEventEntity> Events { get; set; } = new List<MeterEventEntity>();

  [Column("connection_power_w")]
  public float ConnectionPower_W { get; set; } = default!;

  public List<PhaseEntity> Phases { get; set; } = default!;
}

public abstract class MeterEntity<T> : MeterEntity
  where T : MeasurementValidatorEntity
{
  public virtual T MeasurementValidator { get; set; } = default!;
}
