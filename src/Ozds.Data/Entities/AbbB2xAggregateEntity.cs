using System.ComponentModel.DataAnnotations;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class AbbB2xAggregateEntity : MeasurementEntity
{
#pragma warning disable CA1707
  [Required]
  public float VoltageL1Avg_V { get; set; }

  [Required]
  public float VoltageL2Avg_V { get; set; }

  [Required]
  public float VoltageL3Avg_V { get; set; }

  [Required]
  public float CurrentL1Avg_A { get; set; }

  [Required]
  public float CurrentL2Avg_A { get; set; }

  [Required]
  public float CurrentL3Avg_A { get; set; }

  [Required]
  public float ActivePowerL1Avg_W { get; set; }

  [Required]
  public float ActivePowerL2Avg_W { get; set; }

  [Required]
  public float ActivePowerL3Avg_W { get; set; }

  [Required]
  public float ReactivePowerTotalAvg_VAR { get; set; }

  [Required]
  public float ApparentPowerTotalAvg_VA { get; set; }

  [Required]
  public float ActiveEnergyImportTotalMin_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportTotalMax_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportTotalMin_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportTotalMax_Wh { get; set; }

  [Required]
  public float ReactiveEnergyImportTotalMin_VARh { get; set; }

  [Required]
  public float ReactiveEnergyImportTotalMax_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportTotalMin_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportTotalMax_VARh { get; set; }

  [Required]
  public float ActiveEnergyImportTotalT1Min_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportTotalT1Max_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportTotalT2Min_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportTotalT2Max_Wh { get; set; }
#pragma warning restore CA1707
}
