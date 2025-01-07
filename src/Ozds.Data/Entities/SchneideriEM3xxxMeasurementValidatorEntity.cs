using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

namespace Ozds.Data.Entities;

public class
  SchneideriEM3xxxMeasurementValidatorEntity : MeasurementValidatorEntity<
  SchneideriEM3xxxMeterEntity>
{
#pragma warning disable CA1707
  public float MinVoltage_V { get; set; }
  public float MaxVoltage_V { get; set; }
  public float MinCurrent_A { get; set; }
  public float MaxCurrent_A { get; set; }
  public float MinActivePower_W { get; set; }
  public float MaxActivePower_W { get; set; }
  public float MinReactivePower_VAR { get; set; }
  public float MaxReactivePower_VAR { get; set; }
  public float MinApparentPower_VA { get; set; }
  public float MaxApparentPower_VA { get; set; }
#pragma warning restore CA1707
}

public class SchneideriEM3xxxMeasurementValidatorEntityTypeConfiguration :
  EntityTypeConfiguration<SchneideriEM3xxxMeasurementValidatorEntity>
{
  public override void Configure(
    EntityTypeBuilder<SchneideriEM3xxxMeasurementValidatorEntity> builder)
  {
    builder
      .Property(nameof(SchneideriEM3xxxMeasurementValidatorEntity.MinVoltage_V))
      .HasColumnName("min_voltage_v");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementValidatorEntity.MaxVoltage_V))
      .HasColumnName("max_voltage_v");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementValidatorEntity.MinCurrent_A))
      .HasColumnName("min_current_a");

    builder
      .Property(nameof(SchneideriEM3xxxMeasurementValidatorEntity.MaxCurrent_A))
      .HasColumnName("max_current_a");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MinActivePower_W))
      .HasColumnName("min_active_power_w");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MaxActivePower_W))
      .HasColumnName("max_active_power_w");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MinReactivePower_VAR))
      .HasColumnName("min_reactive_power_var");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MaxReactivePower_VAR))
      .HasColumnName("max_reactive_power_var");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MinApparentPower_VA))
      .HasColumnName("min_apparent_power_va");

    builder
      .Property(
        nameof(SchneideriEM3xxxMeasurementValidatorEntity
          .MaxApparentPower_VA))
      .HasColumnName("max_apparent_power_va");
  }
}
