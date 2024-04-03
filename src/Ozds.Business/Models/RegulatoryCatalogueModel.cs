using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;

namespace Ozds.Business.Models;

public class RegulatoryCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float RenewableEnergyFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float BusinessUsageFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float TaxRate_Percent { get; set; }
}
