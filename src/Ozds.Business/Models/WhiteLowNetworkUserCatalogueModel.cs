using System.ComponentModel.DataAnnotations;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models;

public class WhiteLowNetworkUserCatalogueModel : NetworkUserCatalogueModel
{
  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT1Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ActiveEnergyTotalImportT2Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal ReactiveEnergyTotalRampedT0Price_EUR { get; set; }

  [Required]
  [Range(0, uint.MaxValue)]
  public required decimal MeterFeePrice_EUR { get; set; }

  public static WhiteLowNetworkUserCatalogueModel New()
  {
    return new WhiteLowNetworkUserCatalogueModel
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
      ReactiveEnergyTotalRampedT0Price_EUR = 0,
      MeterFeePrice_EUR = 0
    };
  }
}
