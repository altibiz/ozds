using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("abb_b2x_quarter_hourly_aggregate")]
public class AbbB2xQuarterHourlyAggregateEntity : AbbB2xAggregateEntity { }

[Table("abb_b2x_daily_aggregate")]
public class AbbB2xDailyAggregateEntity : AbbB2xAggregateEntity { }

[Table("abb_b2x_monthly_aggregate")]
public class AbbB2xMonthlyAggregateEntity : AbbB2xAggregateEntity { }

public abstract class AbbB2xAggregateEntity : MeasurementEntity
{
#pragma warning disable CA1707
  [Column("voltage_l1_avg_v")]
  [Required]
  public float VoltageL1Avg_V { get; set; }

  [Column("voltage_l2_avg_v")]
  [Required]
  public float VoltageL2Avg_V { get; set; }

  [Column("voltage_l3_avg_v")]
  [Required]
  public float VoltageL3Avg_V { get; set; }

  [Column("current_l1_avg_a")]
  [Required]
  public float CurrentL1Avg_A { get; set; }

  [Column("current_l2_avg_a")]
  [Required]
  public float CurrentL2Avg_A { get; set; }

  [Column("current_l3_avg_a")]
  [Required]
  public float CurrentL3Avg_A { get; set; }

  [Column("active_power_l1_avg_w")]
  [Required]
  public float ActivePowerL1Avg_W { get; set; }

  [Column("active_power_l2_avg_w")]
  [Required]
  public float ActivePowerL2Avg_W { get; set; }

  [Column("active_power_l3_avg_w")]
  [Required]
  public float ActivePowerL3Avg_W { get; set; }

  [Column("reactive_power_total_avg_var")]
  [Required]
  public float ReactivePowerTotalAvg_VAR { get; set; }

  [Column("apparent_power_total_avg_va")]
  [Required]
  public float ApparentPowerTotalAvg_VA { get; set; }

  [Column("active_energy_import_total_min_wh")]
  [Required]
  public float ActiveEnergyImportTotalMin_Wh { get; set; }

  [Column("active_energy_import_total_max_wh")]
  [Required]
  public float ActiveEnergyImportTotalMax_Wh { get; set; }

  [Column("active_energy_export_total_min_wh")]
  [Required]
  public float ActiveEnergyExportTotalMin_Wh { get; set; }

  [Column("active_energy_export_total_max_wh")]
  [Required]
  public float ActiveEnergyExportTotalMax_Wh { get; set; }

  [Column("reactive_energy_import_total_min_varh")]
  [Required]
  public float ReactiveEnergyImportTotalMin_VARh { get; set; }

  [Column("reactive_energy_import_total_max_varh")]
  [Required]
  public float ReactiveEnergyImportTotalMax_VARh { get; set; }

  [Column("reactive_energy_export_total_min_varh")]
  [Required]
  public float ReactiveEnergyExportTotalMin_VARh { get; set; }

  [Column("reactive_energy_export_total_max_varh")]
  [Required]
  public float ReactiveEnergyExportTotalMax_VARh { get; set; }

  [Column("active_energy_import_total_t1_min_wh")]
  [Required]
  public float ActiveEnergyImportTotalT1Min_Wh { get; set; }

  [Column("active_energy_import_total_t1_max_wh")]
  [Required]
  public float ActiveEnergyImportTotalT1Max_Wh { get; set; }

  [Column("active_energy_import_total_t2_min_wh")]
  [Required]
  public float ActiveEnergyImportTotalT2Min_Wh { get; set; }

  [Column("active_energy_import_total_t2_max_wh")]
  [Required]
  public float ActiveEnergyImportTotalT2Max_Wh { get; set; }
#pragma warning restore CA1707
}
