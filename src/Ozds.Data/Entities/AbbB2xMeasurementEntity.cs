using System.ComponentModel.DataAnnotations;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

public class AbbB2xMeasurementEntity : MeasurementEntity
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
  public float ReactivePowerL1_VAR { get; set; }

  [Required]
  public float ReactivePowerL2_VAR { get; set; }

  [Required]
  public float ReactivePowerL3_VAR { get; set; }

  [Required]
  public float ActiveEnergyImportL1_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportL2_Wh { get; set; }

  [Required]
  public float ActiveEnergyImportL3_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportL1_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportL2_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportL3_Wh { get; set; }

  [Required]
  public float ReactiveEnergyImportL1_VARh { get; set; }

  [Required]
  public float ReactiveEnergyImportL2_VARh { get; set; }

  [Required]
  public float ReactiveEnergyImportL3_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportL1_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportL2_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportL3_VARh { get; set; }

  [Required]
  public float ActiveEnergyImportTotal_Wh { get; set; }

  [Required]
  public float ActiveEnergyExportTotal_Wh { get; set; }

  [Required]
  public float ReactiveEnergyImportTotal_VARh { get; set; }

  [Required]
  public float ReactiveEnergyExportTotal_VARh { get; set; }
#pragma warning restore CA1707
}
