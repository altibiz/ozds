using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// TODO: more clear naming for properties

namespace Ozds.Data.Entities;

public class SchneideriEM3xxxMeasurementValidatorEntity : MeasurementValidatorEntity
{
#pragma warning disable CA1707
  [Column("min_voltage_v")]
  [Required]
  public float MinVoltage_V { get; set; }

  [Column("max_voltage_v")]
  [Required]
  public float MaxVoltage_V { get; set; } = default!;

  [Column("min_current_a")]
  [Required]
  public float MinCurrent_A { get; set; } = default!;

  [Column("max_current_a")]
  [Required]
  public float MaxCurrent_A { get; set; } = default!;

  [Column("min_active_power_w")]
  [Required]
  public float MinActivePower_W { get; set; } = default!;

  [Column("max_active_power_w")]
  [Required]
  public float MaxActivePower_W { get; set; } = default!;

  [Column("min_reactive_power_var")]
  [Required]
  public float MinReactivePower_VAR { get; set; } = default!;

  [Column("max_reactive_power_var")]
  [Required]
  public float MaxReactivePower_VAR { get; set; } = default!;

  [Column("min_apparent_power_va")]
  [Required]
  public float MinApparentPower_VA { get; set; } = default!;

  [Column("max_apparent_power_va")]
  [Required]
  public float MaxApparentPower_VA { get; set; } = default!;
#pragma warning restore CA1707
}
