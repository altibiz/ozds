using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ozds.Data.Entities.Base;

namespace Ozds.Data.Entities;

[Table("abb_b2x_measurement")]
public class AbbB2xMeasurementEntity : MeasurementEntity
{
#pragma warning disable CA1707
  [Column("voltage_l1_v")]
  [Required]
  public float VoltageL1_V { get; set; }

  [Column("voltage_l2_v")]
  [Required]
  public float VoltageL2_V { get; set; }

  [Column("voltage_l3_v")]
  [Required]
  public float VoltageL3_V { get; set; }

  [Column("current_l1_a")]
  [Required]
  public float CurrentL1_A { get; set; }

  [Column("current_l2_a")]
  [Required]
  public float CurrentL2_A { get; set; }

  [Column("current_l3_a")]
  [Required]
  public float CurrentL3_A { get; set; }

  [Column("active_power_l1_w")]
  [Required]
  public float ActivePowerL1_W { get; set; }

  [Column("active_power_l2_w")]
  [Required]
  public float ActivePowerL2_W { get; set; }

  [Column("active_power_l3_w")]
  [Required]
  public float ActivePowerL3_W { get; set; }

  [Column("reactive_power_l1_var")]
  [Required]
  public float ReactivePowerL1_VAR { get; set; }

  [Column("reactive_power_l2_var")]
  [Required]
  public float ReactivePowerL2_VAR { get; set; }

  [Column("reactive_power_l3_var")]
  [Required]
  public float ReactivePowerL3_VAR { get; set; }

  [Column("active_energy_import_l1_wh")]
  [Required]
  public float ActiveEnergyImportL1_Wh { get; set; }

  [Column("active_energy_import_l2_wh")]
  [Required]
  public float ActiveEnergyImportL2_Wh { get; set; }

  [Column("active_energy_import_l3_wh")]
  [Required]
  public float ActiveEnergyImportL3_Wh { get; set; }

  [Column("active_energy_export_l1_wh")]
  [Required]
  public float ActiveEnergyExportL1_Wh { get; set; }

  [Column("active_energy_export_l2_wh")]
  [Required]
  public float ActiveEnergyExportL2_Wh { get; set; }

  [Column("active_energy_export_l3_wh")]
  [Required]
  public float ActiveEnergyExportL3_Wh { get; set; }

  [Column("reactive_energy_import_l1_varh")]
  [Required]
  public float ReactiveEnergyImportL1_VARh { get; set; }

  [Column("reactive_energy_import_l2_varh")]
  [Required]
  public float ReactiveEnergyImportL2_VARh { get; set; }

  [Column("reactive_energy_import_l3_varh")]
  [Required]
  public float ReactiveEnergyImportL3_VARh { get; set; }

  [Column("reactive_energy_export_l1_varh")]
  [Required]
  public float ReactiveEnergyExportL1_VARh { get; set; }

  [Column("reactive_energy_export_l2_varh")]
  [Required]
  public float ReactiveEnergyExportL2_VARh { get; set; }

  [Column("reactive_energy_export_l3_varh")]
  [Required]
  public float ReactiveEnergyExportL3_VARh { get; set; }

  [Column("active_energy_import_total_wh")]
  [Required]
  public float ActiveEnergyImportTotal_Wh { get; set; }

  [Column("active_energy_export_total_wh")]
  [Required]
  public float ActiveEnergyExportTotal_Wh { get; set; }

  [Column("reactive_energy_import_total_varh")]
  [Required]
  public float ReactiveEnergyImportTotal_VARh { get; set; }

  [Column("reactive_energy_export_total_varh")]
  [Required]
  public float ReactiveEnergyExportTotal_VARh { get; set; }

  [Column("active_energy_import_total_t1_wh")]
  [Required]
  public float ActiveEnergyImportTotalT1_Wh { get; set; } = default;

  [Column("active_energy_import_total_t2_wh")]
  [Required]
  public float ActiveEnergyImportTotalT2_Wh { get; set; } = default;
#pragma warning restore CA1707
}
