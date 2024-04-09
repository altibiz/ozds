using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RedLowCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ActivePowerTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required decimal MeterFeePrice_EUR { get; set; }

  public static RedLowCatalogueModel New()
  {
    return new RedLowCatalogueModel
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
      ActivePowerTotalImportT1Price_EUR = 0,
      ReactiveEnergyTotalRampedT0Price_EUR = 0,
      MeterFeePrice_EUR = 0
    };
  }
}
