using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("schneider_iem3xxx_aggregates")]
public class SchneideriEM3xxxAggregateEntity : AggregateEntity<SchneideriEM3xxxMeterEntity>
{
#pragma warning disable CA1707
  [Column("voltage_l1_any_t0_avg_v")]
  [Required]
  public float VoltageL1AnyT0Avg_V { get; set; }

  [Column("voltage_l2_any_t0_avg_v")]
  [Required]
  public float VoltageL2AnyT0Avg_V { get; set; }

  [Column("voltage_l3_any_t0_avg_v")]
  [Required]
  public float VoltageL3AnyT0Avg_V { get; set; }

  [Column("current_l1_any_t0_avg_a")]
  [Required]
  public float CurrentL1AnyT0Avg_A { get; set; }

  [Column("current_l2_any_t0_avg_a")]
  [Required]
  public float CurrentL2AnyT0Avg_A { get; set; }

  [Column("current_l3_any_t0_avg_a")]
  [Required]
  public float CurrentL3AnyT0Avg_A { get; set; }

  [Column("active_power_l1_net_t0_avg_w")]
  [Required]
  public float ActivePowerL1NetT0Avg_W { get; set; }

  [Column("active_power_l2_net_t0_avg_w")]
  [Required]
  public float ActivePowerL2NetT0Avg_W { get; set; }

  [Column("active_power_l3_net_t0_avg_w")]
  [Required]
  public float ActivePowerL3NetT0Avg_W { get; set; }

  [Column("reactive_power_total_net_t0_avg_var")]
  [Required]
  public float ReactivePowerTotalNetT0Avg_VAR { get; set; }

  [Column("apparent_power_total_net_t0_avg_va")]
  [Required]
  public float ApparentPowerTotalNetT0Avg_VA { get; set; }

  [Column("active_energy_total_import_t0_min_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT0Min_Wh { get; set; } = default!;

  [Column("active_energy_total_import_t0_max_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT0Max_Wh { get; set; } = default!;

  [Column("active_energy_total_export_t0_min_wh")]
  [Required]
  public decimal ActiveEnergyTotalExportT0Min_Wh { get; set; } = default!;

  [Column("active_energy_total_export_t0_max_wh")]
  [Required]
  public decimal ActiveEnergyTotalExportT0Max_Wh { get; set; } = default!;

  [Column("reactive_energy_total_import_t0_min_varh")]
  [Required]
  public decimal ReactiveEnergyTotalImportT0Min_VARh { get; set; } = default!;

  [Column("reactive_energy_total_import_t0_max_varh")]
  [Required]
  public decimal ReactiveEnergyTotalImportT0Max_VARh { get; set; } = default!;

  [Column("reactive_energy_total_export_t0_min_varh")]
  [Required]
  public decimal ReactiveEnergyTotalExportT0Min_VARh { get; set; } = default!;

  [Column("reactive_energy_total_export_t0_max_varh")]
  [Required]
  public decimal ReactiveEnergyTotalExportT0Max_VARh { get; set; } = default!;

  [Column("active_energy_total_import_t1_min_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT1Min_Wh { get; set; } = default!;

  [Column("active_energy_total_import_t1_max_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT1Max_Wh { get; set; } = default!;

  [Column("active_energy_total_import_t2_min_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT2Min_Wh { get; set; } = default!;

  [Column("active_energy_total_import_t2_max_wh")]
  [Required]
  public decimal ActiveEnergyTotalImportT2Max_Wh { get; set; } = default!;
#pragma warning disable CA1707
}
