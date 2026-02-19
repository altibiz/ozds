using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Context;
using Ozds.Data.Entities.Enums;

namespace Ozds.Data.Entities;

public class MeasurementScopeEntity : ScopeEntity
{
  public IntervalEntity Interval { get; set; }

  public virtual ICollection<RegisterEntity> Registers { get; set; } = default!;
}

public class ApiKeyMeasurementScopeTypeConfiguration
  : EntityTypeConfiguration<MeasurementScopeEntity>
{
  public override void Configure(
    EntityTypeBuilder<MeasurementScopeEntity> builder
  )
  {
    builder
      .HasMany(nameof(MeasurementScopeEntity.Registers))
      .WithOne(nameof(RegisterEntity.Scope));
  }
}
