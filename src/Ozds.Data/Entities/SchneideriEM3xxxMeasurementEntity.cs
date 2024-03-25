using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("schneider_iem3xxx_measurements")]
public class
  SchneideriEM3xxxMeasurementEntity : MeasurementEntity<
  SchneideriEM3xxxMeterEntity>
{
#pragma warning disable CA1707
  [Column("voltage_l1_any_t0_v")]
  [Required]
  public float VoltageL1AnyT0_V { get; set; }

  [Column("voltage_l2_any_t0_v")]
  [Required]
  public float VoltageL2AnyT0_V { get; set; }

  [Column("voltage_l3_any_t0_v")]
  [Required]
  public float VoltageL3AnyT0_V { get; set; }

  [Column("current_l1_any_t0_a")]
  [Required]
  public float CurrentL1AnyT0_A { get; set; }

  [Column("current_l2_any_t0_a")]
  [Required]
  public float CurrentL2AnyT0_A { get; set; }

  [Column("current_l3_any_t0_a")]
  [Required]
  public float CurrentL3AnyT0_A { get; set; }

  [Column("active_power_l1_net_t0_w")]
  [Required]
  public float ActivePowerL1NetT0_W { get; set; }

  [Column("active_power_l2_net_t0_w")]
  [Required]
  public float ActivePowerL2NetT0_W { get; set; }

  [Column("active_power_l3_net_t0_w")]
  [Required]
  public float ActivePowerL3NetT0_W { get; set; }

  [Column("reactive_power_total_net_t0_var")]
  [Required]
  public float ReactivePowerTotalNetT0_VAR { get; set; }

  [Column("apparent_power_total_net_t0_va")]
  [Required]
  public float ApparentPowerTotalNetT0_VA { get; set; }

  [Column("active_energy_import_l1_t0_wh")]
  [Required]
  public float ActiveEnergyL1ImportT0_Wh { get; set; }

  [Column("active_energy_import_l2_t0_wh")]
  [Required]
  public float ActiveEnergyL2ImportT0_Wh { get; set; }

  [Column("active_energy_import_l3_t0_wh")]
  [Required]
  public float ActiveEnergyL3ImportT0_Wh { get; set; }

  [Column("active_energy_import_total_t0_wh")]
  [Required]
  public float ActiveEnergyTotalImportT0_Wh { get; set; }

  [Column("active_energy_export_total_t0_wh")]
  [Required]
  public float ActiveEnergyTotalExportT0_Wh { get; set; }

  [Column("reactive_energy_import_total_t0_varh")]
  [Required]
  public float ReactiveEnergyTotalImportT0_VARh { get; set; }

  [Column("reactive_energy_export_total_t0_varh")]
  [Required]
  public float ReactiveEnergyTotalExportT0_VARh { get; set; }

  [Column("active_energy_import_total_t1_wh")]
  [Required]
  public float ActiveEnergyTotalImportT1_Wh { get; set; } = default;

  [Column("active_energy_import_total_t2_wh")]
  [Required]
  public float ActiveEnergyTotalImportT2_Wh { get; set; } = default;
#pragma warning disable CA1707
}
