using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Attributes;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities.Base;

[Table("meters")]
public abstract class MeterEntity : AuditableEntity
{
  [ForeignKey(nameof(MeasurementLocation))]
  public string MeasurementLocationId { get; set; } = default!;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } = default!;

  [ForeignKey(nameof(Catalogue))]
  public string CatalogueId { get; set; } = default!;

  public virtual CatalogueEntity Catalogue { get; set; } = default!;

  [ForeignKey(nameof(Messenger))]
  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;

  [Column("connection_power_w")]
  public float ConnectionPower_W { get; set; } = default!;

  public List<PhaseEntity> Phases { get; set; } = default!;
}

public abstract class MeterEntity<T> : MeterEntity
  where T : MeasurementValidatorEntity
{
  [ForeignKey(nameof(MeasurementValidator))]
  public string MeasurementValidatorId { get; set; } = default!;

  public virtual T MeasurementValidator { get; set; } = default!;
}
