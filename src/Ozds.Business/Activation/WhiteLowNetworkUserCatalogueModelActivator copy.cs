using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Complex;

public class
  WhiteMediumNetworkUserCatalogueModelActivator : ModelActivator<
  WhiteMediumNetworkUserCatalogueModel>
{
  public override WhiteMediumNetworkUserCatalogueModel ActivateConcrete()
  {
    return New();
  }

  public static WhiteMediumNetworkUserCatalogueModel New()
  {
    return new WhiteMediumNetworkUserCatalogueModel
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
