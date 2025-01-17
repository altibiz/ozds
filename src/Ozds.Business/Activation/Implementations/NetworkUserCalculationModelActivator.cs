using Ozds.Business.Activation.Agnostic;
using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations;

public class NetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      NetworkUserCalculationModel,
      CalculationModel>(serviceProvider)
{
  private readonly AgnosticModelActivator _agnosticModelActivator =
    serviceProvider.GetRequiredService<AgnosticModelActivator>();

  public override void Initialize(NetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.UsageMeterFee = _agnosticModelActivator
      .Activate<UsageMeterFeeCalculationItemModel>();
    model.SupplyActiveEnergyTotalImportT1 = _agnosticModelActivator
      .Activate<SupplyActiveEnergyTotalImportT1CalculationItemModel>();
    model.SupplyActiveEnergyTotalImportT2 = _agnosticModelActivator
      .Activate<SupplyActiveEnergyTotalImportT2CalculationItemModel>();
    model.SupplyBusinessUsageFee = _agnosticModelActivator
      .Activate<SupplyBusinessUsageCalculationItemModel>();
    model.SupplyRenewableEnergyFee = _agnosticModelActivator
      .Activate<SupplyRenewableEnergyCalculationItemModel>();
    model.NetworkUserMeasurementLocationId = "0";
    model.ArchivedNetworkUserMeasurementLocation = default!;
    model.UsageNetworkUserCatalogueId = "0";
    model.SupplyRegulatoryCatalogueId = "0";
    model.ArchivedSupplyRegulatoryCatalogue = default!;
    model.NetworkUserInvoiceId = "0";
    model.UsageFeeTotal_EUR = 0;
    model.SupplyFeeTotal_EUR = 0;
  }
}
