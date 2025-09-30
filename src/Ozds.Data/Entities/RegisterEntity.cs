using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Base;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities;

public class RegisterEntity : TrackableEntity
{
  private Guid _scopeId;

  public string ScopeId
  {
    get { return _scopeId.ToString(); }
    set { _scopeId = Guid.Parse(value); }
  }

  public string Name { get; set; } = default!;

  public virtual MeasurementScopeEntity Scope { get; set; } =
    default!;

  public MeasureEntity Measure { get; set; }

  public OrderOfMagnitudeEntity? OrderOfMagnitude { get; set; }

  public TariffEntity? Tariff { get; set; }

  public DuplexEntity? Duplex { get; set; }

  public PhaseEntity? Phase { get; set; }

  public AggregationEntity? Aggregation { get; set; }
}

public class RegisterTypeConfiguration
  : EntityTypeConfiguration<RegisterEntity>
{
  public override void Configure(
    EntityTypeBuilder<RegisterEntity> builder)
  {
    builder.Ignore(nameof(RegisterEntity.ScopeId));
    builder
      .Property("_scopeId")
      .HasColumnName("scope_id");

    builder
      .HasOne(nameof(RegisterEntity.Scope))
      .WithMany(nameof(MeasurementScopeEntity.Registers))
      .HasForeignKey("_scopeId");
  }
}
