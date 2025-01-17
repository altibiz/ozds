using Ozds.Business.Activation.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Activation.Implementations.Finances;

public class RegulatoryCatalogueModelActivator(
  IServiceProvider serviceProvider
) : InheritingModelActivator<RegulatoryCatalogueModel, AuditableModel>(
  serviceProvider
)
{
  public override void Initialize(RegulatoryCatalogueModel model)
  {
    base.Initialize(model);
    model.ActiveEnergyTotalImportT1Price_EUR = 0;
    model.ActiveEnergyTotalImportT2Price_EUR = 0;
    model.RenewableEnergyFeePrice_EUR = 0;
    model.BusinessUsageFeePrice_EUR = 0;
    model.TaxRate_Percent = 0;
  }
}
