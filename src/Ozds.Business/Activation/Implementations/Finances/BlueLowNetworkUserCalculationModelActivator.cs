using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Finances;

public class BlueLowNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  BlueLowNetworkUserCalculationModel,
  NetworkUserCalculationModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(
    BlueLowNetworkUserCalculationModel model
  )
  {
    base.Initialize(model);

    model.UsageActiveEnergyTotalImportT0 = modelActivator
      .Activate<UsageActiveEnergyTotalImportT0CalculationItemModel>();
    model.UsageReactiveEnergyTotalRampedT0 = modelActivator
      .Activate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>();
    model.ConcreteArchivedUsageNetworkUserCatalogue = default!;
  }
}
