using Ozds.Business.Conversion.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Base;
using Ozds.Data.Entities;
using Ozds.Data.Entities.Base;

namespace Ozds.Business.Conversion;

public class InvoiceModelEntityConverter : ModelEntityConverter<
  IInvoice, InvoiceEntity>
{
  protected override InvoiceEntity ToEntity(
    IInvoice model)
  {
    return model.ToEntity();
  }

  protected override IInvoice ToModel(
    InvoiceEntity entity)
  {
    return entity.ToModel();
  }
}

public static class InvoiceModelEntityConverterExtensions
{
  public static InvoiceModel ToModel(
    this InvoiceEntity entity)
  {
    if (entity is NetworkUserInvoiceEntity networkUserInvoiceEntity)
    {
      return new NetworkUserInvoiceModel
      {
        Id = networkUserInvoiceEntity.Id,
        Title = networkUserInvoiceEntity.Title,
        IssuedOn = networkUserInvoiceEntity.IssuedOn,
        IssuedById = networkUserInvoiceEntity.IssuedById,
        FromDate = networkUserInvoiceEntity.FromDate,
        ToDate = networkUserInvoiceEntity.ToDate,
        NetworkUserId = networkUserInvoiceEntity.NetworkUserId,
        ArchivedLocation = networkUserInvoiceEntity.ArchivedLocation.ToModel(),
        ArchivedNetworkUser = networkUserInvoiceEntity.ArchivedNetworkUser.ToModel(),
        ArchivedRegulatoryCatalogue =
          networkUserInvoiceEntity.ArchivedRegulatoryCatalogue.ToModel(),
        UsageActiveEnergyTotalImportT0Fee_EUR =
          networkUserInvoiceEntity.UsageActiveEnergyTotalImportT0Fee_EUR,
        UsageActiveEnergyTotalImportT1Fee_EUR =
          networkUserInvoiceEntity.UsageActiveEnergyTotalImportT1Fee_EUR,
        UsageActiveEnergyTotalImportT2Fee_EUR =
          networkUserInvoiceEntity.UsageActiveEnergyTotalImportT2Fee_EUR,
        UsageActivePowerTotalImportT1PeakFee_EUR =
          networkUserInvoiceEntity.UsageActivePowerTotalImportT1PeakFee_EUR,
        UsageReactiveEnergyTotalRampedT0Fee_EUR =
          networkUserInvoiceEntity.UsageReactiveEnergyTotalRampedT0Fee_EUR,
        UsageMeterFee_EUR = networkUserInvoiceEntity.UsageMeterFee_EUR,
        UsageFeeTotal_EUR = networkUserInvoiceEntity.UsageFeeTotal_EUR,
        SupplyActiveEnergyTotalImportT1Fee_EUR =
          networkUserInvoiceEntity.SupplyActiveEnergyTotalImportT1Fee_EUR,
        SupplyActiveEnergyTotalImportT2Fee_EUR =
          networkUserInvoiceEntity.SupplyActiveEnergyTotalImportT2Fee_EUR,
        SupplyBusinessUsageFee_EUR = networkUserInvoiceEntity.SupplyBusinessUsageFee_EUR,
        SupplyRenewableEnergyFee_EUR = networkUserInvoiceEntity.SupplyRenewableEnergyFee_EUR,
        SupplyFeeTotal_EUR = networkUserInvoiceEntity.SupplyFeeTotal_EUR,
        Total_EUR = networkUserInvoiceEntity.Total_EUR,
        TaxRate_Percent = networkUserInvoiceEntity.TaxRate_Percent,
        Tax_EUR = networkUserInvoiceEntity.Tax_EUR,
        TotalWithTax_EUR = networkUserInvoiceEntity.TotalWithTax_EUR
      };
    }

    throw new NotSupportedException(
      $"NetworkUserInvoiceEntity type {entity.GetType().Name} is not supported."
    );
  }

  public static InvoiceEntity ToEntity(
    this IInvoice model)
  {
    if (model is NetworkUserInvoiceModel networkUserInvoiceModel)
    {
      return new NetworkUserInvoiceEntity
      {
        Id = networkUserInvoiceModel.Id,
        Title = networkUserInvoiceModel.Title,
        IssuedOn = networkUserInvoiceModel.IssuedOn,
        IssuedById = networkUserInvoiceModel.IssuedById,
        FromDate = networkUserInvoiceModel.FromDate,
        ToDate = networkUserInvoiceModel.ToDate,
        NetworkUserId = networkUserInvoiceModel.NetworkUserId,
        ArchivedLocation = networkUserInvoiceModel.ArchivedLocation.ToEntity(),
        ArchivedNetworkUser = networkUserInvoiceModel.ArchivedNetworkUser.ToEntity(),
        ArchivedRegulatoryCatalogue =
          networkUserInvoiceModel.ArchivedRegulatoryCatalogue.ToEntity(),
        UsageActiveEnergyTotalImportT0Fee_EUR =
          networkUserInvoiceModel.UsageActiveEnergyTotalImportT0Fee_EUR,
        UsageActiveEnergyTotalImportT1Fee_EUR =
          networkUserInvoiceModel.UsageActiveEnergyTotalImportT1Fee_EUR,
        UsageActiveEnergyTotalImportT2Fee_EUR =
          networkUserInvoiceModel.UsageActiveEnergyTotalImportT2Fee_EUR,
        UsageActivePowerTotalImportT1PeakFee_EUR =
          networkUserInvoiceModel.UsageActivePowerTotalImportT1PeakFee_EUR,
        UsageReactiveEnergyTotalRampedT0Fee_EUR =
          networkUserInvoiceModel.UsageReactiveEnergyTotalRampedT0Fee_EUR,
        UsageMeterFee_EUR = networkUserInvoiceModel.UsageMeterFee_EUR,
        UsageFeeTotal_EUR = networkUserInvoiceModel.UsageFeeTotal_EUR,
        SupplyActiveEnergyTotalImportT1Fee_EUR =
          networkUserInvoiceModel.SupplyActiveEnergyTotalImportT1Fee_EUR,
        SupplyActiveEnergyTotalImportT2Fee_EUR =
          networkUserInvoiceModel.SupplyActiveEnergyTotalImportT2Fee_EUR,
        SupplyBusinessUsageFee_EUR = networkUserInvoiceModel.SupplyBusinessUsageFee_EUR,
        SupplyRenewableEnergyFee_EUR = networkUserInvoiceModel.SupplyRenewableEnergyFee_EUR,
        SupplyFeeTotal_EUR = networkUserInvoiceModel.SupplyFeeTotal_EUR,
        Total_EUR = networkUserInvoiceModel.Total_EUR,
        TaxRate_Percent = networkUserInvoiceModel.TaxRate_Percent,
        Tax_EUR = networkUserInvoiceModel.Tax_EUR,
        TotalWithTax_EUR = networkUserInvoiceModel.TotalWithTax_EUR
      };
    }

    throw new NotSupportedException(
      $"NetworkUserInvoiceModel type {model.GetType().Name} is not supported."
    );
  }
}
