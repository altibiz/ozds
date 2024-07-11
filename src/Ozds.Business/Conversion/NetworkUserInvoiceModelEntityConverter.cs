using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Data.Entities;

namespace Ozds.Business.Conversion;

public class NetworkUserInvoiceModelEntityConverter : ModelEntityConverter<
  NetworkUserInvoiceModel, NetworkUserInvoiceEntity>
{
  protected override NetworkUserInvoiceEntity ToEntity(
    NetworkUserInvoiceModel model)
  {
    return model.ToEntity();
  }

  protected override NetworkUserInvoiceModel ToModel(
    NetworkUserInvoiceEntity entity)
  {
    return entity.ToModel();
  }
}

public static class NetworkUserInvoiceModelEntityConverterExtensions
{
  public static NetworkUserInvoiceModel ToModel(
    this NetworkUserInvoiceEntity entity)
  {
    return new NetworkUserInvoiceModel
    {
      Id = entity.Id,
      Title = entity.Title,
      IssuedOn = entity.IssuedOn,
      IssuedById = entity.IssuedById,
      FromDate = entity.FromDate,
      ToDate = entity.ToDate,
      NetworkUserId = entity.NetworkUserId,
      ArchivedLocation = entity.ArchivedLocation.ToModel(),
      ArchivedNetworkUser = entity.ArchivedNetworkUser.ToModel(),
      ArchivedRegulatoryCatalogue =
        entity.ArchivedRegulatoryCatalogue.ToModel(),
      UsageActiveEnergyTotalImportT0Fee_EUR =
        entity.UsageActiveEnergyTotalImportT0Fee_EUR,
      UsageActiveEnergyTotalImportT1Fee_EUR =
        entity.UsageActiveEnergyTotalImportT1Fee_EUR,
      UsageActiveEnergyTotalImportT2Fee_EUR =
        entity.UsageActiveEnergyTotalImportT2Fee_EUR,
      UsageActivePowerTotalImportT1PeakFee_EUR =
        entity.UsageActivePowerTotalImportT1PeakFee_EUR,
      UsageReactiveEnergyTotalRampedT0Fee_EUR =
        entity.UsageReactiveEnergyTotalRampedT0Fee_EUR,
      UsageMeterFee_EUR = entity.UsageMeterFee_EUR,
      UsageFeeTotal_EUR = entity.UsageFeeTotal_EUR,
      SupplyActiveEnergyTotalImportT1Fee_EUR =
        entity.SupplyActiveEnergyTotalImportT1Fee_EUR,
      SupplyActiveEnergyTotalImportT2Fee_EUR =
        entity.SupplyActiveEnergyTotalImportT2Fee_EUR,
      SupplyBusinessUsageFee_EUR = entity.SupplyBusinessUsageFee_EUR,
      SupplyRenewableEnergyFee_EUR = entity.SupplyRenewableEnergyFee_EUR,
      SupplyFeeTotal_EUR = entity.SupplyFeeTotal_EUR,
      Total_EUR = entity.Total_EUR,
      TaxRate_Percent = entity.TaxRate_Percent,
      Tax_EUR = entity.Tax_EUR,
      TotalWithTax_EUR = entity.TotalWithTax_EUR
    };
  }

  public static NetworkUserInvoiceEntity ToEntity(
    this NetworkUserInvoiceModel model)
  {
    return new NetworkUserInvoiceEntity
    {
      Id = model.Id,
      Title = model.Title,
      IssuedOn = model.IssuedOn,
      IssuedById = model.IssuedById,
      FromDate = model.FromDate,
      ToDate = model.ToDate,
      NetworkUserId = model.NetworkUserId,
      ArchivedLocation = model.ArchivedLocation.ToEntity(),
      ArchivedNetworkUser = model.ArchivedNetworkUser.ToEntity(),
      ArchivedRegulatoryCatalogue =
        model.ArchivedRegulatoryCatalogue.ToEntity(),
      UsageActiveEnergyTotalImportT0Fee_EUR =
        model.UsageActiveEnergyTotalImportT0Fee_EUR,
      UsageActiveEnergyTotalImportT1Fee_EUR =
        model.UsageActiveEnergyTotalImportT1Fee_EUR,
      UsageActiveEnergyTotalImportT2Fee_EUR =
        model.UsageActiveEnergyTotalImportT2Fee_EUR,
      UsageActivePowerTotalImportT1PeakFee_EUR =
        model.UsageActivePowerTotalImportT1PeakFee_EUR,
      UsageReactiveEnergyTotalRampedT0Fee_EUR =
        model.UsageReactiveEnergyTotalRampedT0Fee_EUR,
      UsageMeterFee_EUR = model.UsageMeterFee_EUR,
      UsageFeeTotal_EUR = model.UsageFeeTotal_EUR,
      SupplyActiveEnergyTotalImportT1Fee_EUR =
        model.SupplyActiveEnergyTotalImportT1Fee_EUR,
      SupplyActiveEnergyTotalImportT2Fee_EUR =
        model.SupplyActiveEnergyTotalImportT2Fee_EUR,
      SupplyBusinessUsageFee_EUR = model.SupplyBusinessUsageFee_EUR,
      SupplyRenewableEnergyFee_EUR = model.SupplyRenewableEnergyFee_EUR,
      SupplyFeeTotal_EUR = model.SupplyFeeTotal_EUR,
      Total_EUR = model.Total_EUR,
      TaxRate_Percent = model.TaxRate_Percent,
      Tax_EUR = model.Tax_EUR,
      TotalWithTax_EUR = model.TotalWithTax_EUR
    };
  }
}
