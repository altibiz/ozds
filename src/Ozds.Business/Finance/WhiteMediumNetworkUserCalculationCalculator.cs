using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Finance.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
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

  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    WhiteMediumNetworkUserCatalogueModel usageCatalogue,
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var supplyCatalogue = calculationBasis.SupplyRegulatoryCatalogue;

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
      ArchivedUsageNetworkUserCatalogue = usageCatalogue,
      ArchivedSupplyRegulatoryCatalogue =
        calculationBasis.SupplyRegulatoryCatalogue,
      UsageActiveEnergyTotalImportT1 = _calculationItemCalculator
        .Calculate<UsageActiveEnergyTotalImportT1CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            usageCatalogue.ActiveEnergyTotalImportT1Price_EUR
          )
        ),
      UsageActiveEnergyTotalImportT2 = _calculationItemCalculator
        .Calculate<UsageActiveEnergyTotalImportT2CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            usageCatalogue.ActiveEnergyTotalImportT2Price_EUR
          )
        ),
      UsageActivePowerTotalImportT1Peak = _calculationItemCalculator
        .Calculate<UsageActivePowerTotalImportT1PeakCalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            usageCatalogue.ActivePowerTotalImportT1Price_EUR
          )
        ),
      UsageReactiveEnergyTotalRampedT0 = _calculationItemCalculator
        .Calculate<UsageReactiveEnergyTotalRampedT0CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            usageCatalogue.ReactiveEnergyTotalRampedT0Price_EUR
          )
        ),
      UsageMeterFee = _calculationItemCalculator
        .Calculate<UsageMeterFeeCalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            usageCatalogue.MeterFeePrice_EUR
          )
        ),
      SupplyActiveEnergyTotalImportT1 = _calculationItemCalculator
        .Calculate<SupplyActiveEnergyTotalImportT1CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            supplyCatalogue.ActiveEnergyTotalImportT1Price_EUR
          )
        ),
      SupplyActiveEnergyTotalImportT2 = _calculationItemCalculator
        .Calculate<SupplyActiveEnergyTotalImportT2CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            supplyCatalogue.ActiveEnergyTotalImportT2Price_EUR
          )
        ),
      SupplyBusinessUsageFee = _calculationItemCalculator
        .Calculate<SupplyBusinessUsageFeeCalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            supplyCatalogue.BusinessUsageFeePrice_EUR
          )
        ),
      SupplyRenewableEnergyFee = _calculationItemCalculator
        .Calculate<SupplyRenewableEnergyFeeCalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            supplyCatalogue.RenewableEnergyFeePrice_EUR
          )
        ),
    };

    return initial;
  }
}
