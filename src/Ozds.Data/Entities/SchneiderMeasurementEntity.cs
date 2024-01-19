using System.ComponentModel.DataAnnotations.Schema;

namespace Ozds.Data.Entities;

public class SchneiderMeasurementEntity : HypertableEntity
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
  public decimal ReactivePowerTotal_VAR { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ApparentPowerTotal_VA { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyImportL1_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyImportL2_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyImportL3_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyImportTotal_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ActiveEnergyExportTotal_Wh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactiveEnergyImportTotal_VARh { get; set; } = default;

  [Column(TypeName = "double precision")]
  public decimal ReactiveEnergyExportTotal_VARh { get; set; } = default;
}
