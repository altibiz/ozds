using Ozds.Business.Activation.Base;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;

namespace Ozds.Business.Activation.Implementations.Finances;

public class NetworkUserCalculationModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<
      NetworkUserCalculationModel,
      CalculationModel>(serviceProvider)
{
  private readonly ModelActivator modelActivator =
    serviceProvider.GetRequiredService<ModelActivator>();

  public override void Initialize(NetworkUserCalculationModel model)
  {
    base.Initialize(model);

    model.UsageMeterFee = modelActivator
      .Activate<UsageMeterFeeCalculationItemModel>();
    model.SupplyActiveEnergyTotalImportT1 = modelActivator
      .Activate<SupplyActiveEnergyTotalImportT1CalculationItemModel>();
    model.SupplyActiveEnergyTotalImportT2 = modelActivator
      .Activate<SupplyActiveEnergyTotalImportT2CalculationItemModel>();
    model.SupplyBusinessUsageFee = modelActivator
      .Activate<SupplyBusinessUsageCalculationItemModel>();
    model.SupplyRenewableEnergyFee = modelActivator
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
