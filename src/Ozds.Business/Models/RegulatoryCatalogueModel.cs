using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RegulatoryCatalogueModel : AuditableModel
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal RenewableEnergyFeePrice_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal BusinessUsageFeePrice_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal TaxRate_Percent { get; set; }
}
