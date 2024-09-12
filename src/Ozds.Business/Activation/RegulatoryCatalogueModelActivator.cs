using Ozds.Business.Activation.Base;
using Ozds.Business.Models;

namespace Ozds.Business.Activation.Complex;

public class
  RegulatoryCatalogueModelActivator : ModelActivator<RegulatoryCatalogueModel>
{
  public override RegulatoryCatalogueModel ActivateConcrete()
  {
    return New();
  }

  public static RegulatoryCatalogueModel New()
  {
    return new RegulatoryCatalogueModel
    {
      Id = default!,
      Title = "",
      CreatedOn = DateTimeOffset.UtcNow,
      CreatedById = default,
      LastUpdatedOn = default,
      LastUpdatedById = default,
      IsDeleted = false,
      DeletedOn = default,
      DeletedById = default,
      ActiveEnergyTotalImportT1Price_EUR = 0,
      ActiveEnergyTotalImportT2Price_EUR = 0,
      RenewableEnergyFeePrice_EUR = 0,
      BusinessUsageFeePrice_EUR = 0,
      TaxRate_Percent = 0
    };
  }
}
