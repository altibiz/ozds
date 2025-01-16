using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation;

public class NetworkUserInvoiceModelActivator(IServiceProvider serviceProvider)
  : InheritingModelActivator<NetworkUserInvoiceModel, InvoiceModel>(
    serviceProvider
  )
{
  public override void Initialize(NetworkUserInvoiceModel model)
  {
    base.Initialize(model);

    model.BillId = default!;
    model.NetworkUserId = default!;
    model.ArchivedLocation = default!;
    model.ArchivedNetworkUser = default!;
    model.ArchivedRegulatoryCatalogue = default!;
    model.UsageActiveEnergyTotalImportT0Fee_EUR = 0;
    model.UsageActiveEnergyTotalImportT1Fee_EUR = 0;
    model.UsageActiveEnergyTotalImportT2Fee_EUR = 0;
    model.UsageActivePowerTotalImportT1PeakFee_EUR = 0;
    model.UsageReactiveEnergyTotalRampedT0Fee_EUR = 0;
    model.UsageMeterFee_EUR = 0;
    model.UsageFeeTotal_EUR = 0;
    model.SupplyActiveEnergyTotalImportT1Fee_EUR = 0;
    model.SupplyActiveEnergyTotalImportT2Fee_EUR = 0;
    model.SupplyBusinessUsageFee_EUR = 0;
    model.SupplyRenewableEnergyFee_EUR = 0;
    model.SupplyFeeTotal_EUR = 0;
  }
}
