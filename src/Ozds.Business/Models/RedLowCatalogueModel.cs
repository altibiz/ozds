using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class RedLowCatalogueModel : CatalogueModel
{
  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float MaxActivePowerTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float ReactiveEnergyTotalImportT0Price_EUR { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public required float MeterFeePrice_EUR { get; set; }

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
      MaxActivePowerTotalImportT1Price_EUR = 0,
      ReactiveEnergyTotalImportT0Price_EUR = 0,
      MeterFeePrice_EUR = 0
    };
  }
}
