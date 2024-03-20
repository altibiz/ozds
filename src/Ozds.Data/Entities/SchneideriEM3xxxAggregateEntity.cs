using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("schneider_iem3xxx_quarter_hourly_aggregate")]
public class SchneideriEM3xxxQuarterHourlyAggregateEntity : SchneideriEM3xxxAggregateEntity { }

[Table("schneider_iem3xxx_daily_aggregate")]
public class SchneideriEM3xxxDailyAggregateEntity : SchneideriEM3xxxAggregateEntity { }

[Table("schneider_iem3xxx_monthly_aggregate")]
public class SchneideriEM3xxxMonthlyAggregateEntity : SchneideriEM3xxxAggregateEntity { }

public abstract class SchneideriEM3xxxAggregateEntity : MeasurementEntity
{
#pragma warning disable CA1707
  [Required]
  public float VoltageL1_V { get; set; }

  [Required]
  public float VoltageL2_V { get; set; }

  [Required]
  public float VoltageL3_V { get; set; }

  [Required]
  public float CurrentL1_A { get; set; }

  [Required]
  public float CurrentL2_A { get; set; }

  [Required]
  public float CurrentL3_A { get; set; }

  [Required]
  public float ActivePowerL1_W { get; set; }

  [Required]
  public float ActivePowerL2_W { get; set; }

  [Required]
  public float ActivePowerL3_W { get; set; }

  [Required]
  public float ReactivePowerTotal_VAR { get; set; }

  [Required]
  public float ApparentPowerTotal_VA { get; set; }

  [Required]
  public float ActiveEnergyImportL1_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportL2_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportL3_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportTotal_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportTotal_Wh { get; set; }

  [Required]
  public float ReactiveEnergyImportTotal_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportTotal_VARh { get; set; }
#pragma warning disable CA1707
}
