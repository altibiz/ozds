using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion.Implementations.Finances;

public class NetworkUserInvoiceModelEntityConverter(
  IServiceProvider serviceProvider
) : InheritingModelEntityConverter<
      NetworkUserInvoiceModel,
      InvoiceModel,
      NetworkUserInvoiceEntity,
      InvoiceEntity>(serviceProvider)
{
  private readonly ModelEntityConverter modelEntityConverter =
    serviceProvider.GetRequiredService<ModelEntityConverter>();

  public override void InitializeEntity(
    NetworkUserInvoiceModel model,
    NetworkUserInvoiceEntity entity
  )
  {
    base.InitializeEntity(model, entity);
    entity.BillId = model.BillId;
    entity.LocationId = model.LocationId;
    entity.ArchivedLocation =
      model.ArchivedLocation is null
        ? null!
        : modelEntityConverter.ToEntity<LocationEntity>(
            model.ArchivedLocation);
    entity.NetworkUserId = model.NetworkUserId;
    entity.ArchivedNetworkUser =
      model.ArchivedNetworkUser is null
        ? null!
        : modelEntityConverter.ToEntity<NetworkUserEntity>(
            model.ArchivedNetworkUser);
    entity.ArchivedRegulatoryCatalogue =
      model.ArchivedRegulatoryCatalogue.ToEntity();
    entity.UsageActiveEnergyTotalImportT0Fee_EUR =
      model.UsageActiveEnergyTotalImportT0Fee_EUR;
    entity.UsageActiveEnergyTotalImportT1Fee_EUR =
      model.UsageActiveEnergyTotalImportT1Fee_EUR;
    entity.UsageActiveEnergyTotalImportT2Fee_EUR =
      model.UsageActiveEnergyTotalImportT2Fee_EUR;
    entity.UsageActivePowerTotalImportT1PeakFee_EUR =
      model.UsageActivePowerTotalImportT1PeakFee_EUR;
    entity.UsageReactiveEnergyTotalRampedT0Fee_EUR =
      model.UsageReactiveEnergyTotalRampedT0Fee_EUR;
    entity.UsageMeterFee_EUR = model.UsageMeterFee_EUR;
    entity.UsageFeeTotal_EUR = model.UsageFeeTotal_EUR;
    entity.SupplyActiveEnergyTotalImportT1Fee_EUR =
      model.SupplyActiveEnergyTotalImportT1Fee_EUR;
    entity.SupplyActiveEnergyTotalImportT2Fee_EUR =
      model.SupplyActiveEnergyTotalImportT2Fee_EUR;
    entity.SupplyBusinessUsageFee_EUR = model.SupplyBusinessUsageFee_EUR;
    entity.SupplyRenewableEnergyFee_EUR = model.SupplyRenewableEnergyFee_EUR;
    entity.SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR;
    entity.Total_EUR = model.Total_EUR;
    entity.InvoiceTaxRate_Percent = model.InvoiceTaxRate_Percent;
    entity.InvoiceTax_EUR = model.InvoiceTax_EUR;
    entity.InvoiceTotalWithTax_EUR = model.InvoiceTotalWithTax_EUR;
  }

  public override void InitializeModel(
    NetworkUserInvoiceEntity entity,
    NetworkUserInvoiceModel model
  )
  {
    base.InitializeModel(entity, model);
    model.BillId = entity.BillId;
    model.LocationId = entity.LocationId;
    model.ArchivedLocation =
      entity.ArchivedLocation is null
        ? null!
        : modelEntityConverter.ToModel<LocationModel>(
            entity.ArchivedLocation);
    model.NetworkUserId = entity.NetworkUserId;
    model.ArchivedNetworkUser =
      entity.ArchivedNetworkUser is null
        ? null!
        : modelEntityConverter.ToModel<NetworkUserModel>(
            entity.ArchivedNetworkUser);
    model.ArchivedRegulatoryCatalogue =
      entity.ArchivedRegulatoryCatalogue is null
        ? null!
        : modelEntityConverter.ToModel<RegulatoryCatalogueModel>(
            entity.ArchivedRegulatoryCatalogue);
    model.UsageActiveEnergyTotalImportT0Fee_EUR =
      entity.UsageActiveEnergyTotalImportT0Fee_EUR;
    model.UsageActiveEnergyTotalImportT1Fee_EUR =
      entity.UsageActiveEnergyTotalImportT1Fee_EUR;
    model.UsageActiveEnergyTotalImportT2Fee_EUR =
      entity.UsageActiveEnergyTotalImportT2Fee_EUR;
    model.UsageActivePowerTotalImportT1PeakFee_EUR =
      entity.UsageActivePowerTotalImportT1PeakFee_EUR;
    model.UsageReactiveEnergyTotalRampedT0Fee_EUR =
      entity.UsageReactiveEnergyTotalRampedT0Fee_EUR;
    model.UsageMeterFee_EUR = entity.UsageMeterFee_EUR;
    model.UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR;
    model.SupplyActiveEnergyTotalImportT1Fee_EUR =
      entity.SupplyActiveEnergyTotalImportT1Fee_EUR;
    model.SupplyActiveEnergyTotalImportT2Fee_EUR =
      entity.SupplyActiveEnergyTotalImportT2Fee_EUR;
    model.SupplyBusinessUsageFee_EUR = entity.SupplyBusinessUsageFee_EUR;
    model.SupplyRenewableEnergyFee_EUR = entity.SupplyRenewableEnergyFee_EUR;
    model.SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR;
    model.Total_EUR = entity.Total_EUR;
    model.InvoiceTaxRate_Percent = entity.InvoiceTaxRate_Percent;
    model.InvoiceTax_EUR = entity.InvoiceTax_EUR;
    model.InvoiceTotalWithTax_EUR = entity.InvoiceTotalWithTax_EUR;
  }
}
