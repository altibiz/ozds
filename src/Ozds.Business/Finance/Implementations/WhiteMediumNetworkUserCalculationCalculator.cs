using Ozds.Business.Finance.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Implementations;

public class
  WhiteMediumNetworkUserCalculationCalculator(
    CalculationItemCalculator calculationItemCalculator)
  : NetworkUserCalculationCalculator
  <
    WhiteMediumNetworkUserCatalogueModel>
{
  private readonly CalculationItemCalculator
    _calculationItemCalculator =
      calculationItemCalculator;

  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    WhiteMediumNetworkUserCatalogueModel usageCatalogue,
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var supplyCatalogue = calculationBasis.SupplyRegulatoryCatalogue;

    var usageActiveEnergyTotalImportT1 = _calculationItemCalculator
      .Calculate<UsageActiveEnergyTotalImportT1CalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = usageCatalogue.ActiveEnergyTotalImportT1Price_EUR
        }
      );

    var usageActiveEnergyTotalImportT2 = _calculationItemCalculator
      .Calculate<UsageActiveEnergyTotalImportT2CalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = usageCatalogue.ActiveEnergyTotalImportT2Price_EUR
        }
      );

    var usageActivePowerTotalImportT1Peak = _calculationItemCalculator
      .Calculate<UsageActivePowerTotalImportT1PeakCalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = usageCatalogue.ActivePowerTotalImportT1Price_EUR
        }
      );

    var usageReactiveEnergyTotalRampedT0 = _calculationItemCalculator
      .Calculate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = usageCatalogue.ReactiveEnergyTotalRampedT0Price_EUR
        }
      );

    var usageMeterFee = _calculationItemCalculator
      .Calculate<UsageMeterFeeCalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = usageCatalogue.MeterFeePrice_EUR
        }
      );

    var usageFeeTotal = System.Math.Round(
      usageActiveEnergyTotalImportT1.Total
      + usageActiveEnergyTotalImportT2.Total
      + usageActivePowerTotalImportT1Peak.Total
      + usageReactiveEnergyTotalRampedT0.Total
      + usageMeterFee.Total,
      2);

    var supplyActiveEnergyTotalImportT1 = _calculationItemCalculator
      .Calculate<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = supplyCatalogue.ActiveEnergyTotalImportT1Price_EUR
        }
      );

    var supplyActiveEnergyTotalImportT2 = _calculationItemCalculator
      .Calculate<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = supplyCatalogue.ActiveEnergyTotalImportT2Price_EUR
        }
      );

    var supplyBusinessUsageFee = _calculationItemCalculator
      .Calculate<SupplyBusinessUsageCalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = supplyCatalogue.BusinessUsageFeePrice_EUR
        }
      );

    var supplyRenewableEnergyFee = _calculationItemCalculator
      .Calculate<SupplyRenewableEnergyCalculationItemModel>(
        new CalculationItemBasisModel
        {
          Aggregates = calculationBasis.Aggregates,
          Price_EUR = supplyCatalogue.RenewableEnergyFeePrice_EUR
        }
      );

    var supplyFeeTotal = System.Math.Round(
      supplyActiveEnergyTotalImportT1.Total
      + supplyActiveEnergyTotalImportT2.Total
      + supplyBusinessUsageFee.Total
      + supplyRenewableEnergyFee.Total,
      2);

    var total = System.Math.Round(usageFeeTotal + supplyFeeTotal, 2);

    var initial = new WhiteMediumNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"{usageCatalogue.Title} calculation for "
        + $"{calculationBasis.NetworkUser.Title} at "
        + $"{calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.ToDate,
      FromDate = calculationBasis.FromDate,
      NetworkUserInvoiceId = "0",
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
      UsageActiveEnergyTotalImportT1 = usageActiveEnergyTotalImportT1,
      UsageActiveEnergyTotalImportT2 = usageActiveEnergyTotalImportT2,
      UsageActivePowerTotalImportT1Peak = usageActivePowerTotalImportT1Peak,
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
