using Ozds.Business.Conversion.Base;
using Ozds.Business.Models.Base;
using Ozds.Document.Entities;

namespace Ozds.Business.Conversion.Implementations.Document;

public class NetworkUserCalculationModelDocumentEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelDocumentEntityConverter<
  NetworkUserCalculationModel,
  CalculationModel,
  NetworkUserCalculationEntity,
  CalculationEntity>(serviceProvider)
{
  private readonly ModelDocumentEntityConverter modelDocumentEntityConverter =
    serviceProvider.GetRequiredService<ModelDocumentEntityConverter>();

  public override void InitializeEntity(
    NetworkUserCalculationModel model,
    NetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.MeterTitle = model.ArchivedMeter.Title;
    entity.MeterId = model.MeterId;
    entity.MeasurementLocationTitle =
      model.ArchivedNetworkUserMeasurementLocation.Title;
    entity.MeasurementLocationId = model.NetworkUserMeasurementLocationId;
    entity.UsageMeterFee = modelDocumentEntityConverter
      .ToEntity<UsageMeterFeeCalculationItemEntity>(
        model.UsageMeterFee);
    entity.SupplyActiveEnergyTotalImportT1 = modelDocumentEntityConverter
      .ToEntity<SupplyActiveEnergyTotalImportT1CalculationItemEntity>(
        model.SupplyActiveEnergyTotalImportT1);
    entity.SupplyActiveEnergyTotalImportT2 = modelDocumentEntityConverter
      .ToEntity<SupplyActiveEnergyTotalImportT2CalculationItemEntity>(
        model.SupplyActiveEnergyTotalImportT2);
    entity.SupplyBusinessUsageFee = modelDocumentEntityConverter
      .ToEntity<SupplyBusinessUsageCalculationItemEntity>(
        model.SupplyBusinessUsageFee);
    entity.SupplyRenewableEnergyFee = modelDocumentEntityConverter
      .ToEntity<SupplyRenewableEnergyCalculationItemEntity>(
        model.SupplyRenewableEnergyFee);
    entity.UsageNetworkUserCatalogueId = model.UsageNetworkUserCatalogueId;
    entity.SupplyRegulatoryCatalogueId = model.SupplyRegulatoryCatalogueId;
    entity.NetworkUserInvoiceId = model.NetworkUserInvoiceId;
    entity.SupplyRegulatoryCatalogue = modelDocumentEntityConverter
      .ToEntity<RegulatoryCatalogueEntity>(
        model.ArchivedSupplyRegulatoryCatalogue);
    entity.UsageFeeTotal_EUR = model.UsageFeeTotal_EUR;
    entity.SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR;
  }
}
