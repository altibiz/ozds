using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RegulatoryCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal RenewableEnergyFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal BusinessUsageFeePrice_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal TaxRate_Percent { get; set; }

  public static RegulatoryCatalogueModel New()
  {
    return new RegulatoryCatalogueModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      ActiveEnergyTotalImportT1Price_EUR = 0,
      ActiveEnergyTotalImportT2Price_EUR = 0,
      RenewableEnergyFeePrice_EUR = 0,
      BusinessUsageFeePrice_EUR = 0,
      TaxRate_Percent = 0
    };
  }
}
