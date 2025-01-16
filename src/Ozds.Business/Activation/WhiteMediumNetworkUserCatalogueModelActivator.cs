using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class WhiteMediumNetworkUserCatalogueModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  WhiteMediumNetworkUserCatalogueModel,
  NetworkUserCatalogueModel>(serviceProvider)
{
  public override void Initialize(WhiteMediumNetworkUserCatalogueModel model)
  {
    base.Initialize(model);
    model.ActiveEnergyTotalImportT1Price_EUR = 0;
    model.ActiveEnergyTotalImportT2Price_EUR = 0;
    model.ActivePowerTotalImportT1Price_EUR = 0;
    model.ReactiveEnergyTotalRampedT0Price_EUR = 0;
    model.MeterFeePrice_EUR = 0;
  }
}
