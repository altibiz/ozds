using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Complex;

public class
  WhiteLowNetworkUserCatalogueModelActivator : ModelActivator<
  WhiteLowNetworkUserCatalogueModel>
{
  public override WhiteLowNetworkUserCatalogueModel ActivateConcrete()
  {
    return New();
  }

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
