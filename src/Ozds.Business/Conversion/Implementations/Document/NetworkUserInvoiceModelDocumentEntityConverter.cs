using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class NetworkUserInvoiceModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  NetworkUserInvoiceModel,
  InvoiceModel,
  NetworkUserInvoiceEntity,
  InvoiceEntity>(serviceProvider)
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    NetworkUserInvoiceModel model,
    NetworkUserInvoiceEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.BillId = model.BillId;
    entity.Location = modelDocumentEntityConverter
      .ToEntity<LocationEntity>(model.ArchivedLocation);
    entity.NetworkUser = modelDocumentEntityConverter
      .ToEntity<NetworkUserEntity>(model.ArchivedNetworkUser);
    entity.RegulatoryCatalogue = modelDocumentEntityConverter
      .ToEntity<RegulatoryCatalogueEntity>(model.ArchivedRegulatoryCatalogue);
    entity.UsageActiveEnergyTotalImportT0Fee_EUR = model.UsageActiveEnergyTotalImportT0Fee_EUR;
    entity.UsageActiveEnergyTotalImportT1Fee_EUR = model.UsageActiveEnergyTotalImportT1Fee_EUR;
    entity.UsageActiveEnergyTotalImportT2Fee_EUR = model.UsageActiveEnergyTotalImportT2Fee_EUR;
    entity.UsageActivePowerTotalImportT1PeakFee_EUR = model.UsageActivePowerTotalImportT1PeakFee_EUR;
    entity.UsageReactiveEnergyTotalRampedT0Fee_EUR = model.UsageReactiveEnergyTotalRampedT0Fee_EUR;
    entity.UsageMeterFee_EUR = model.UsageMeterFee_EUR;
    entity.UsageFeeTotal_EUR = model.UsageFeeTotal_EUR;
    entity.SupplyActiveEnergyTotalImportT1Fee_EUR = model.SupplyActiveEnergyTotalImportT1Fee_EUR;
    entity.SupplyActiveEnergyTotalImportT2Fee_EUR = model.SupplyActiveEnergyTotalImportT2Fee_EUR;
    entity.SupplyBusinessUsageFee_EUR = model.SupplyBusinessUsageFee_EUR;
    entity.SupplyRenewableEnergyFee_EUR = model.SupplyRenewableEnergyFee_EUR;
    entity.SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR;
    entity.NetworkUserId = model.NetworkUserId;
  }
}
