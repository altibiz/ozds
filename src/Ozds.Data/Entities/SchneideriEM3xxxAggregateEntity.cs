using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("schneider_iem3xxx_quarter_hourly_aggregate")]
public class SchneideriEM3xxxQuarterHourlyAggregateEntity : SchneideriEM3xxxAggregateEntity
{
  public TimeSpan Interval => TimeSpan.FromMinutes(15);
}

[Table("schneider_iem3xxx_daily_aggregate")]
public class SchneideriEM3xxxDailyAggregateEntity : SchneideriEM3xxxAggregateEntity
{
  public TimeSpan Interval => TimeSpan.FromDays(1);
}

[Table("schneider_iem3xxx_monthly_aggregate")]
public class SchneideriEM3xxxMonthlyAggregateEntity : SchneideriEM3xxxAggregateEntity
{
  public TimeSpan Interval => TimeSpan.FromDays(30);
}

public abstract class SchneideriEM3xxxAggregateEntity : MeasurementEntity
{
#pragma warning disable CA1707
  [Column("voltage_l1_avg_v")]
  [Required]
  public decimal VoltageL1Avg_V { get; set; } = default!;

  [Column("voltage_l2_avg_v")]
  [Required]
  public decimal VoltageL2Avg_V { get; set; } = default!;

  [Column("voltage_l3_avg_v")]
  [Required]
  public decimal VoltageL3Avg_V { get; set; } = default!;

  [Column("current_l1_avg_a")]
  [Required]
  public decimal CurrentL1Avg_A { get; set; } = default!;

  [Column("current_l2_avg_a")]
  [Required]
  public decimal CurrentL2Avg_A { get; set; } = default!;

  [Column("current_l3_avg_a")]
  [Required]
  public decimal CurrentL3Avg_A { get; set; } = default!;

  [Column("active_power_l1_avg_w")]
  [Required]
  public decimal ActivePowerL1Avg_W { get; set; } = default!;

  [Column("active_power_l2_avg_w")]
  [Required]
  public decimal ActivePowerL2Avg_W { get; set; } = default!;

  [Column("active_power_l3_avg_w")]
  [Required]
  public decimal ActivePowerL3Avg_W { get; set; } = default!;

  [Column("reactive_power_total_avg_w")]
  [Required]
  public decimal ReactivePowerTotalAvg_VAR { get; set; } = default!;

  [Column("apparent_power_total_avg_va")]
  [Required]
  public decimal ApparentPowerTotalAvg_VA { get; set; } = default!;

  [Column("active_energy_import_total_min_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalMin_Wh { get; set; } = default!;

  [Column("active_energy_import_total_max_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalMax_Wh { get; set; } = default!;

  [Column("active_energy_export_total_min_wh")]
  [Required]
  public decimal ActiveEnergyExportTotalMin_Wh { get; set; } = default!;

  [Column("active_energy_export_total_max_wh")]
  [Required]
  public decimal ActiveEnergyExportTotalMax_Wh { get; set; } = default!;

  [Column("reactive_energy_import_total_min_varh")]
  [Required]
  public decimal ReactiveEnergyImportTotalMin_VARh { get; set; } = default!;

  [Column("reactive_energy_import_total_max_varh")]
  [Required]
  public decimal ReactiveEnergyImportTotalMax_VARh { get; set; } = default!;

  [Column("reactive_energy_export_total_min_varh")]
  [Required]
  public decimal ReactiveEnergyExportTotalMin_VARh { get; set; } = default!;

  [Column("reactive_energy_export_total_max_varh")]
  [Required]
  public decimal ReactiveEnergyExportTotalMax_VARh { get; set; } = default!;

  [Column("active_energy_import_total_t1_min_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalT1Min_Wh { get; set; } = default!;

  [Column("active_energy_import_total_t1_max_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalT1Max_Wh { get; set; } = default!;

  [Column("active_energy_import_total_t2_min_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalT2Min_Wh { get; set; } = default!;

  [Column("active_energy_import_total_t2_max_wh")]
  [Required]
  public decimal ActiveEnergyImportTotalT2Max_Wh { get; set; } = default!;
#pragma warning disable CA1707
}
