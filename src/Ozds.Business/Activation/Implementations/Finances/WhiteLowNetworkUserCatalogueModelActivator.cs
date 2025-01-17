using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class WhiteLowNetworkUserCatalogueModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  WhiteLowNetworkUserCatalogueModel,
  NetworkUserCatalogueModel>(serviceProvider)
{
  public override void Initialize(WhiteLowNetworkUserCatalogueModel model)
  {
    base.Initialize(model);
    model.ActiveEnergyTotalImportT1Price_EUR = 0;
    model.ActiveEnergyTotalImportT2Price_EUR = 0;
    model.ReactiveEnergyTotalRampedT0Price_EUR = 0;
    model.MeterFeePrice_EUR = 0;
  }
}
