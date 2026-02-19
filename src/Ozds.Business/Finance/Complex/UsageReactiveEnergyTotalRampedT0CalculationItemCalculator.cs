using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using static Ozds.Business.Extensions.PrimitiveRoundingExtensions;

namespace Ozds.Business.Finance.Complex;

public class UsageReactiveEnergyTotalRampedT0CalculationItemCalculator
  : CalculationItemCalculator<UsageReactiveEnergyTotalRampedT0CalculationItemModel>
{
  protected override UsageReactiveEnergyTotalRampedT0CalculationItemModel CalculateConcrete(
    CalculationItemBasisModel calculationBasis
  )
  {
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
      {
        ReactiveImportMin_kVARh = 0,
        ReactiveImportMax_kVARh = 0,
        ReactiveImportAmount_kVARh = 0,
        ReactiveExportMin_kVARh = 0,
        ReactiveExportMax_kVARh = 0,
        ReactiveExportAmount_kVARh = 0,
        ActiveImportMin_kWh = 0,
        ActiveImportMax_kWh = 0,
        ActiveImportAmount_kWh = 0,
        Price_EUR = calculationBasis.Price_EUR,
        Amount_kVARh = 0,
        Total_EUR = 0,
      };
    }

    var aggregates = calculationBasis
      .Aggregates.OrderBy(a => a.Timestamp)
      .ToList();

    var reactiveImportMin = aggregates
      .First()
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var reactiveImportMinKilo = Round(reactiveImportMin / 1000M, 2);

    var reactiveImportMax = aggregates
      .Last()
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var reactiveImportMaxKilo = Round(reactiveImportMax / 1000M, 2);

    var reactiveImportAmountKilo = Round(
      reactiveImportMaxKilo - reactiveImportMinKilo,
      0
    );

    var reactiveExportMin = aggregates
      .First()
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexExport()
      .AggregateMin()
      .PhaseSum();

    var reactiveExportMinKilo = Round(reactiveExportMin / 1000M, 2);

    var reactiveExportMax = aggregates
      .Last()
      .ReactiveEnergy_VARh.TariffUnary()
      .DuplexExport()
      .AggregateMin()
      .PhaseSum();

    var reactiveExportMaxKilo = Round(reactiveExportMax / 1000M, 2);

    var reactiveExportAmountKilo = Round(
      reactiveExportMaxKilo - reactiveExportMinKilo,
      0
    );

    var activeImportMin = aggregates
      .First()
      .ActiveEnergy_Wh.TariffUnary()
      .DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var activeImportMinKilo = Round(activeImportMin / 1000M, 2);

    var activeImportMax = aggregates
      .Last()
      .ActiveEnergy_Wh.TariffUnary()
      .DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var activeImportMaxKilo = Round(activeImportMax / 1000M, 2);

    var activeImportAmountKilo = Round(
      activeImportMaxKilo - activeImportMinKilo,
      0
    );

    var amountKilo = Round(
      System.Math.Max(
        System.Math.Abs(reactiveImportAmountKilo)
          + System.Math.Abs(reactiveExportAmountKilo)
          - 0.33M * activeImportAmountKilo,
        0
      ),
      0
    );

    var price = Round(calculationBasis.Price_EUR, 6);

    var total = Round(amountKilo * price, 2);

    return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ReactiveImportMin_kVARh = reactiveImportMinKilo,
      ReactiveImportMax_kVARh = reactiveImportMaxKilo,
      ReactiveImportAmount_kVARh = reactiveImportAmountKilo,
      ReactiveExportMin_kVARh = reactiveExportMinKilo,
      ReactiveExportMax_kVARh = reactiveExportMaxKilo,
      ReactiveExportAmount_kVARh = reactiveExportAmountKilo,
      ActiveImportMin_kWh = activeImportMinKilo,
      ActiveImportMax_kWh = activeImportMaxKilo,
      ActiveImportAmount_kWh = activeImportAmountKilo,
      Price_EUR = price,
      Amount_kVARh = amountKilo,
      Total_EUR = total,
    };
  }
}
