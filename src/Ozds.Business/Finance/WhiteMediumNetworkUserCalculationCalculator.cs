using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Finance.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Abstractions;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance;

public class
  WhiteMediumNetworkUserCalculationCalculator : NetworkUserCalculationCalculator
<
  WhiteMediumNetworkUserCatalogueModel>
{
  public readonly AgnosticCalculationItemCalculator _calculationItemCalculator;

  public WhiteMediumNetworkUserCalculationCalculator(
    AgnosticCalculationItemCalculator calculationItemCalculator)
  {
    _calculationItemCalculator = calculationItemCalculator;
  }

  protected override INetworkUserCalculation CalculateForNetworkUser(
    WhiteMediumNetworkUserCatalogueModel usageCatalogue,
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var supplyCatalogue = calculationBasis.SupplyRegulatoryCatalogue;

    var usageActiveEnergyTotalImportT1 = _calculationItemCalculator
      .Calculate<UsageActiveEnergyTotalImportT1CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.ActiveEnergyTotalImportT1Price_EUR
        )
      );

    var usageActiveEnergyTotalImportT2 = _calculationItemCalculator
      .Calculate<UsageActiveEnergyTotalImportT2CalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.ActiveEnergyTotalImportT2Price_EUR
        )
      );

    var usageActivePowerTotalImportT1Peak = _calculationItemCalculator
      .Calculate<UsageActivePowerTotalImportT1PeakCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          usageCatalogue.ActivePowerTotalImportT1Price_EUR
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

    var usageFeeTotal =
      usageActiveEnergyTotalImportT1.Total_EUR
      + usageActiveEnergyTotalImportT2.Total_EUR
      + usageActivePowerTotalImportT1Peak.Total_EUR
      + usageReactiveEnergyTotalRampedT0.Total_EUR
      + usageMeterFee.Total_EUR;

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
      .Calculate<SupplyBusinessUsageFeeCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.BusinessUsageFeePrice_EUR
        )
      );

    var supplyRenewableEnergyFee = _calculationItemCalculator
      .Calculate<SupplyRenewableEnergyFeeCalculationItemModel>(
        new CalculationItemBasisModel(
          calculationBasis.Aggregates,
          supplyCatalogue.RenewableEnergyFeePrice_EUR
        )
      );

    var supplyFeeTotal =
      supplyActiveEnergyTotalImportT1.Total_EUR
      + supplyActiveEnergyTotalImportT2.Total_EUR
      + supplyBusinessUsageFee.Total_EUR
      + supplyRenewableEnergyFee.Total_EUR;

    var initial = new WhiteMediumNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"${usageCatalogue.Title} calculation for {calculationBasis.NetworkUser.Title} at {calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.ToDate,
      FromDate = calculationBasis.FromDate,
      NetworkUserInvoiceId = calculationBasis.NetworkUser.Id,
      UsageNetworkUserCatalogueId = usageCatalogue.Id,
      SupplyRegulatoryCatalogueId =
        calculationBasis.SupplyRegulatoryCatalogue.Id,
      MeasurementLocationId = calculationBasis.MeasurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = default!,
      ArchivedMeter = calculationBasis.Meter,
      ArchivedMeasurementLocation = calculationBasis.MeasurementLocation,
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
      Total_EUR = usageFeeTotal + supplyFeeTotal
    };

    return initial;
  }
}
