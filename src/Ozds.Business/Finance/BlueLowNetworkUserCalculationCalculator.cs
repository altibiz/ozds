using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Finance.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance;

public class
  BlueLowNetworkUserCalculationCalculator(
    AgnosticCalculationItemCalculator calculationItemCalculator)
  : NetworkUserCalculationCalculator<
    BlueLowNetworkUserCatalogueModel>
{
  private readonly AgnosticCalculationItemCalculator
    _calculationItemCalculator =
      calculationItemCalculator;

  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    BlueLowNetworkUserCatalogueModel usageCatalogue,
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var supplyCatalogue = calculationBasis.SupplyRegulatoryCatalogue;

    var usageActiveEnergyTotalImportT0 = _calculationItemCalculator
      .Calculate<UsageActiveEnergyTotalImportT0CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.ActiveEnergyTotalImportT0Price_EUR
        )
      );

    var usageReactiveEnergyTotalRampedT0 = _calculationItemCalculator
      .Calculate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.ReactiveEnergyTotalRampedT0Price_EUR
        )
      );

    var usageMeterFee = _calculationItemCalculator
      .Calculate<UsageMeterFeeCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.MeterFeePrice_EUR
        )
      );

    var usageFeeTotal = System.Math.Round(
      usageActiveEnergyTotalImportT0.Total
      + usageReactiveEnergyTotalRampedT0.Total
      + usageMeterFee.Total,
      2);

    var supplyActiveEnergyTotalImportT1 = _calculationItemCalculator
      .Calculate<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.ActiveEnergyTotalImportT1Price_EUR
        )
      );

    var supplyActiveEnergyTotalImportT2 = _calculationItemCalculator
      .Calculate<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.ActiveEnergyTotalImportT2Price_EUR
        )
      );

    var supplyBusinessUsageFee = _calculationItemCalculator
      .Calculate<SupplyBusinessUsageCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.BusinessUsageFeePrice_EUR
        )
      );

    var supplyRenewableEnergyFee = _calculationItemCalculator
      .Calculate<SupplyRenewableEnergyCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.RenewableEnergyFeePrice_EUR
        )
      );

    var supplyFeeTotal = System.Math.Round(
      supplyActiveEnergyTotalImportT1.Total
      + supplyActiveEnergyTotalImportT2.Total
      + supplyBusinessUsageFee.Total
      + supplyRenewableEnergyFee.Total,
      2);

    var total = System.Math.Round(usageFeeTotal + supplyFeeTotal, 2);

    var initial = new BlueLowNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"{usageCatalogue.Title} calculation for "
        + $"{calculationBasis.NetworkUser.Title} at "
        + $"{calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.ToDate,
      FromDate = calculationBasis.FromDate,
      NetworkUserInvoiceId = default!,
      UsageNetworkUserCatalogueId = usageCatalogue.Id,
      SupplyRegulatoryCatalogueId =
        calculationBasis.SupplyRegulatoryCatalogue.Id,
      NetworkUserMeasurementLocationId =
        calculationBasis.MeasurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = default!,
      ArchivedMeter = calculationBasis.Meter,
      ArchivedNetworkUserMeasurementLocation =
        calculationBasis.MeasurementLocation,
      ConcreteArchivedUsageNetworkUserCatalogue = usageCatalogue,
      ArchivedSupplyRegulatoryCatalogue =
        calculationBasis.SupplyRegulatoryCatalogue,
      UsageActiveEnergyTotalImportT0 = usageActiveEnergyTotalImportT0,
      UsageReactiveEnergyTotalRampedT0 = usageReactiveEnergyTotalRampedT0,
      UsageMeterFee = usageMeterFee,
      UsageFeeTotal_EUR = usageFeeTotal,
      SupplyActiveEnergyTotalImportT1 = supplyActiveEnergyTotalImportT1,
      SupplyActiveEnergyTotalImportT2 = supplyActiveEnergyTotalImportT2,
      SupplyBusinessUsageFee = supplyBusinessUsageFee,
      SupplyRenewableEnergyFee = supplyRenewableEnergyFee,
      SupplyFeeTotal_EUR = supplyFeeTotal,
      Total_EUR = total
    };

    return initial;
  }
}
