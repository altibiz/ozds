using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Finance.Base;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance;

public class
  RedLowNetworkUserCalculationCalculator : NetworkUserCalculationCalculator<
  RedLowNetworkUserCatalogueModel>
{
  public readonly AgnosticCalculationItemCalculator _calculationItemCalculator;

  public RedLowNetworkUserCalculationCalculator(
    AgnosticCalculationItemCalculator calculationItemCalculator)
  {
    _calculationItemCalculator = calculationItemCalculator;
  }

  protected override NetworkUserCalculationModel CalculateForNetworkUser(
    RedLowNetworkUserCatalogueModel catalogue,
    NetworkUserCalculationBasisModel calculationBasis
  )
  {
    var initial = new RedLowNetworkUserCalculationModel
    {
      Id = default!,
      Title =
        $"${catalogue.Title} calculation for {calculationBasis.NetworkUser.Title} at {calculationBasis.Location.Title}",
      MeterId = calculationBasis.Meter.Id,
      ToDate = calculationBasis.ToDate,
      FromDate = calculationBasis.FromDate,
      NetworkUserInvoiceId = calculationBasis.NetworkUser.Id,
      UsageNetworkUserCatalogueId = catalogue.Id,
      SupplyRegulatoryCatalogueId =
        calculationBasis.SupplyRegulatoryCatalogue.Id,
      MeasurementLocationId = calculationBasis.MeasurementLocation.Id,
      IssuedOn = DateTimeOffset.UtcNow,
      IssuedById = default!,
      ArchivedMeter = calculationBasis.Meter,
      ArchivedMeasurementLocation = calculationBasis.MeasurementLocation,
      ArchivedUsageNetworkUserCatalogue = catalogue,
      ArchivedSupplyRegulatoryCatalogue =
        calculationBasis.SupplyRegulatoryCatalogue,
      ActiveEnergyTotalImportT1 = _calculationItemCalculator
        .Calculate<ActiveEnergyTotalImportT1CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            catalogue.ActiveEnergyTotalImportT1Price_EUR
          )
        ),
      ActiveEnergyTotalImportT2 = _calculationItemCalculator
        .Calculate<ActiveEnergyTotalImportT2CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            catalogue.ActiveEnergyTotalImportT2Price_EUR
          )
        ),
      ActivePowerTotalImportT1Peak = _calculationItemCalculator
        .Calculate<ActivePowerTotalImportT1PeakCalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            catalogue.ActivePowerTotalImportT1Price_EUR
          )
        ),
      ReactiveEnergyTotalRampedT0 = _calculationItemCalculator
        .Calculate<ReactiveEnergyTotalRampedT0CalculationItemModel>(
          new CalculationItemBasisModel(
            calculationBasis.Aggregates,
            catalogue.ReactiveEnergyTotalRampedT0Price_EUR
          )
        ),
      MeterFeePrice_EUR = catalogue.MeterFeePrice_EUR
    };

    return initial;
  }
}
