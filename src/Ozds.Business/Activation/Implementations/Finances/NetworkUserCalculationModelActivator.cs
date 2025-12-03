using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class NetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
  NetworkUserCalculationModel,
  CalculationModel>(serviceProvider)
{
  public override void Initialize(NetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.MeterId = "0";
    model.ArchivedMeter = default!;
    model.NetworkUserMeasurementLocationId = "0";
    model.ArchivedNetworkUserMeasurementLocation = default!;
    model.UsageNetworkUserCatalogueId = "0";
    model.SupplyRegulatoryCatalogueId = "0";
    model.ArchivedSupplyRegulatoryCatalogue = default!;
    model.NetworkUserInvoiceId = "0";
  }
}
