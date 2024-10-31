using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ozds.Data.Entities.Base;
using Ozds.Data.Extensions;

// TODO: more clear naming for properties

namespace Ozds.Data.Entities;

public class
  AbbB2xMeasurementValidatorEntity : MeasurementValidatorEntity<
  AbbB2xMeterEntity>
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
#pragma warning restore CA1707
}

public class
  AbbB2xMeasurementValidatorEntityTypeConfiguration : EntityTypeConfiguration<
  AbbB2xMeasurementValidatorEntity>
{
  public override void Configure(
    EntityTypeBuilder<AbbB2xMeasurementValidatorEntity> builder)
  {
    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MinVoltage_V))
      .HasColumnName("min_voltage_v");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MaxVoltage_V))
      .HasColumnName("max_voltage_v");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MinCurrent_A))
      .HasColumnName("min_current_a");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MaxCurrent_A))
      .HasColumnName("max_current_a");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MinActivePower_W))
      .HasColumnName("min_active_power_w");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MaxActivePower_W))
      .HasColumnName("max_active_power_w");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MinReactivePower_VAR))
      .HasColumnName("min_reactive_power_var");

    builder
      .Property(nameof(AbbB2xMeasurementValidatorEntity.MaxReactivePower_VAR))
      .HasColumnName("max_reactive_power_var");
  }
}
