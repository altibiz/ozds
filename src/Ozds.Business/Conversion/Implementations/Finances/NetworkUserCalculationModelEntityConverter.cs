using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserCalculationModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NetworkUserCalculationModel,
      CalculationModel,
      NetworkUserCalculationEntity,
      CalculationEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    NetworkUserCalculationModel model,
    NetworkUserCalculationEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    model.UsageMeterFee =
      entity.UsageMeterFee is null
        ? null!
        : modelEntityConverter
            .ToModel<UsageMeterFeeCalculationItemModel>(
              entity.UsageMeterFee);
    model.SupplyActiveEnergyTotalImportT1 =
      entity.SupplyActiveEnergyTotalImportT1 is null
        ? null!
        : modelEntityConverter
            .ToModel<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
              entity.SupplyActiveEnergyTotalImportT1);
    model.SupplyActiveEnergyTotalImportT2 =
      entity.SupplyActiveEnergyTotalImportT2 is null
        ? null!
        : modelEntityConverter
            .ToModel<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
              entity.SupplyActiveEnergyTotalImportT2);
    model.SupplyBusinessUsageFee =
      entity.SupplyBusinessUsageFee is null
        ? null!
        : modelEntityConverter
            .ToModel<SupplyBusinessUsageCalculationItemModel>(
              entity.SupplyBusinessUsageFee);
    model.SupplyRenewableEnergyFee =
      entity.SupplyRenewableEnergyFee is null
        ? null!
        : modelEntityConverter
            .ToModel<SupplyRenewableEnergyCalculationItemModel>(
              entity.SupplyRenewableEnergyFee);
    model.NetworkUserMeasurementLocationId =
      entity.NetworkUserMeasurementLocationId;
    model.ArchivedNetworkUserMeasurementLocation =
      entity.ArchivedNetworkUserMeasurementLocation is null
        ? null!
        : modelEntityConverter
            .ToModel<NetworkUserMeasurementLocationModel>(
              entity.ArchivedNetworkUserMeasurementLocation);
    model.SupplyRegulatoryCatalogueId =
      entity.SupplyRegulatoryCatalogueId;
    model.ArchivedSupplyRegulatoryCatalogue =
      entity.ArchivedSupplyRegulatoryCatalogue is null
        ? null!
        : modelEntityConverter
            .ToModel<RegulatoryCatalogueModel>(
              entity.ArchivedSupplyRegulatoryCatalogue);
    model.NetworkUserInvoiceId = entity.NetworkUserInvoiceId;
    model.Kind = entity.Kind;
    model.UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR;
    model.SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR;
    model.Total_EUR = entity.Total_EUR;
  }

  public override void InitializeModel(
    NetworkUserCalculationEntity entity,
    NetworkUserCalculationModel model
  )
  {
    base.InitializeModel(entity, model);
    model.UsageMeterFee = entity.UsageMeterFee is null
      ? null!
      : modelEntityConverter
          .ToModel<UsageMeterFeeCalculationItemModel>(
            entity.UsageMeterFee);
    model.SupplyActiveEnergyTotalImportT1 = entity.SupplyActiveEnergyTotalImportT1 is null
      ? null!
      : modelEntityConverter
          .ToModel<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
            entity.SupplyActiveEnergyTotalImportT1);
    model.SupplyActiveEnergyTotalImportT2 = entity.SupplyActiveEnergyTotalImportT2 is null
      ? null!
      : modelEntityConverter
          .ToModel<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
            entity.SupplyActiveEnergyTotalImportT2);
    model.SupplyBusinessUsageFee = entity.SupplyBusinessUsageFee is null
      ? null!
      : modelEntityConverter
          .ToModel<SupplyBusinessUsageCalculationItemModel>(
            entity.SupplyBusinessUsageFee);
    model.SupplyRenewableEnergyFee = entity.SupplyRenewableEnergyFee is null
      ? null!
      : modelEntityConverter
          .ToModel<SupplyRenewableEnergyCalculationItemModel>(
            entity.SupplyRenewableEnergyFee);
    model.NetworkUserMeasurementLocationId =
      entity.NetworkUserMeasurementLocationId;
    model.ArchivedNetworkUserMeasurementLocation =
      entity.ArchivedNetworkUserMeasurementLocation is null
        ? null!
        : modelEntityConverter
            .ToModel<NetworkUserMeasurementLocationModel>(
              entity.ArchivedNetworkUserMeasurementLocation);
    model.SupplyRegulatoryCatalogueId =
      entity.SupplyRegulatoryCatalogueId;
    model.ArchivedSupplyRegulatoryCatalogue =
      entity.ArchivedSupplyRegulatoryCatalogue is null
        ? null!
        : modelEntityConverter
            .ToModel<RegulatoryCatalogueModel>(
              entity.ArchivedSupplyRegulatoryCatalogue);
    model.NetworkUserInvoiceId = entity.NetworkUserInvoiceId;
    model.Kind = entity.Kind;
    model.UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR;
    model.SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR;
    model.Total_EUR = entity.Total_EUR;
  }
}
