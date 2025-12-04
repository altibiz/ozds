using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class BlackoutNetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  BlackoutNetworkUserCalculationModel,
  NetworkUserCalculationModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(BlackoutNetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.ConcreteArchivedUsageNetworkUserCatalogue =
      modelActivator
        .Activate<NetworkUserCatalogueModel>();
  }
}
