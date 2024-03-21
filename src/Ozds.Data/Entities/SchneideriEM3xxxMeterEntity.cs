using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class SchneideriEM3xxxMeterEntity : MeterEntity
{
#pragma warning disable CA1707
  [Column("min_voltage_v")]
  public float MinVoltage_V { get; set; }

  [Column("max_voltage_v")]
  public float MaxVoltage_V { get; set; } = default!;

  [Column("min_current_a")]
  public float MinCurrent_A { get; set; } = default!;

  [Column("max_current_a")]
  public float MaxCurrent_A { get; set; } = default!;

  [Column("min_active_power_w")]
  public float MinActivePower_W { get; set; } = default!;

  [Column("max_active_power_w")]
  public float MaxActivePower_W { get; set; } = default!;

  [Column("min_reactive_power_var")]
  public float MinReactivePower_VAR { get; set; } = default!;

  [Column("max_reactive_power_var")]
  public float MaxReactivePower_VAR { get; set; } = default!;

  [Column("min_apparent_power_var")]
  public float MinApparentPower_VAR { get; set; } = default!;

  [Column("max_apparent_power_var")]
  public float MaxApparentPower_VAR { get; set; } = default!;

  [Column("connection_power_w")]
  public float ConnectionPower_W { get; set; } = default!;

  public List<Phase> Phases { get; set; } = default!;
#pragma warning restore CA1707
}
