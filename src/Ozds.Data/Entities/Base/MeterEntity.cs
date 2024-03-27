using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Enums;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities.Base;

public class MeterEntity : AuditableEntity
{
  [ForeignKey(nameof(MeasurementLocation))]
  [Column(TypeName = "bigint")]
  public string MeasurementLocationId { get; set; } = default!;

  public virtual MeasurementLocationEntity MeasurementLocation { get; set; } =
    default!;

  [ForeignKey(nameof(Catalogue))]
  [Column(TypeName = "bigint")]
  public string CatalogueId { get; set; } = default!;

  public virtual CatalogueEntity Catalogue { get; set; } = default!;

  [ForeignKey(nameof(Messenger))]
  [Column(TypeName = "bigint")]
  public string MessengerId { get; set; } = default!;

  public virtual MessengerEntity Messenger { get; set; } = default!;

  [Column("connection_power_w")]
  public float ConnectionPower_W { get; set; } = default!;

  public List<PhaseEntity> Phases { get; set; } = default!;
}

public class MeterEntity<T> : MeterEntity
  where T : MeasurementValidatorEntity
{
  [ForeignKey(nameof(MeasurementValidator))]
  [Column(TypeName = "bigint")]
  public string MeasurementValidatorId { get; set; } = default!;

  public virtual T MeasurementValidator { get; set; } = default!;
}

public class MeterInheritedEntityTypeConfiguration : ConcreteHierarchyEntityTypeConfiguration<MeterEntity>
{
  public override void Configure<T>(EntityTypeBuilder<T> builder)
  {
    builder
      .UseTphMappingStrategy()
      .ToTable("meters")
      .HasDiscriminator<string>("kind");
  }
}
