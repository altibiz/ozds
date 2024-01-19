using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities;

public class AbbMeasurementEntity : HypertableEntity
{
  [Column(TypeName = "double precision")]
  public decimal VoltageL1_V { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal VoltageL2_V { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal VoltageL3_V { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal CurrentL1_A { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal CurrentL2_A { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal CurrentL3_A { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerL1_W { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerL2_W { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerL3_W { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerL1_VAR { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerL2_VAR { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerL3_VAR { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerImportL1_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerImportL2_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerImportL3_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerExportL1_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerExportL2_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActivePowerExportL3_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerImportL1_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerImportL2_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerImportL3_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerExportL1_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerExportL2_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactivePowerExportL3_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyImportTotal_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyExportTotal_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactiveEnergyImportTotal_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactiveEnergyExportTotal_VARh { get; set; } = default;
}
