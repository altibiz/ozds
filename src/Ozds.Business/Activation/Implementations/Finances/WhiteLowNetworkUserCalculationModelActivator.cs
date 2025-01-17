using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Finances;

public class WhiteLowNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      WhiteLowNetworkUserCalculationModel,
      NetworkUserCalculationModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(WhiteLowNetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.UsageActiveEnergyTotalImportT1 = modelActivator
      .Activate<UsageActiveEnergyTotalImportT1CalculationItemModel>();
    model.UsageActiveEnergyTotalImportT2 = modelActivator
      .Activate<UsageActiveEnergyTotalImportT2CalculationItemModel>();
    model.UsageReactiveEnergyTotalRampedT0 = modelActivator
      .Activate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>();
    model.ConcreteArchivedUsageNetworkUserCatalogue = default!;
  }
}
