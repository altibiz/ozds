using Ozds.Business.Finance.Base;
using Ozds.Business.Math;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageReactiveEnergyTotalRampedT0CalculationItemCalculator :
  CalculationItemCalculator<
    UsageReactiveEnergyTotalRampedT0CalculationItemModel>
{
  protected override UsageReactiveEnergyTotalRampedT0CalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
      {
        ReactiveImportMin_VARh = 0,
        ReactiveImportMax_VARh = 0,
        ReactiveImportAmount_VARh = 0,
        ReactiveExportMin_VARh = 0,
        ReactiveExportMax_VARh = 0,
        ReactiveExportAmount_VARh = 0,
        ActiveImportMin_Wh = 0,
        ActiveImportMax_Wh = 0,
        ActiveImportAmount_Wh = 0,
        Price_EUR = calculationBasis.Price,
        Amount_VARh = 0,
        Total_EUR = 0
      };
    }

    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var reactiveImportMinOriginal = aggregates
      .First().ReactiveEnergy_VARh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var reactiveImportMin = System.Math.Round(reactiveImportMinOriginal, 2);

    var reactiveImportMaxOriginal = aggregates
      .Last().ReactiveEnergy_VARh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var reactiveImportMax = System.Math.Round(reactiveImportMaxOriginal, 2);

    var reactiveImportAmount = System.Math.Round(
      reactiveImportMax - reactiveImportMin,
      0);

    var reactiveExportMinOriginal = aggregates
      .First().ReactiveEnergy_VARh
      .TariffUnary()
      .DuplexExport()
      .PhaseSum();

    var reactiveExportMin = System.Math.Round(reactiveExportMinOriginal, 2);

    var reactiveExportMaxOriginal = aggregates
      .Last().ReactiveEnergy_VARh
      .TariffUnary()
      .DuplexExport()
      .PhaseSum();

    var reactiveExportMax = System.Math.Round(reactiveExportMaxOriginal, 2);

    var reactiveExportAmount = System.Math.Round(
      reactiveExportMax - reactiveExportMin,
      0);

    var activeImportMinOriginal = aggregates
      .First().ActiveEnergy_Wh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var activeImportMin = System.Math.Round(activeImportMinOriginal, 2);

    var activeImportMaxOriginal = aggregates
      .Last().ActiveEnergy_Wh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var activeImportMax = System.Math.Round(activeImportMaxOriginal, 2);

    var activeImportAmount = System.Math.Round(
      activeImportMax - activeImportMin,
      0);

    var amount = System.Math.Round(
      System.Math.Max(
        System.Math.Abs(reactiveImportAmount)
        + System.Math.Abs(reactiveExportAmount)
        - 0.33M * activeImportAmount,
        0),
      0);

    var price = System.Math.Round(
      calculationBasis.Price,
      6);

    var total = System.Math.Round(
      amount * price,
      2);

    return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ReactiveImportMin_VARh = reactiveImportMin,
      ReactiveImportMax_VARh = reactiveImportMax,
      ReactiveImportAmount_VARh = reactiveImportAmount,
      ReactiveExportMin_VARh = reactiveExportMin,
      ReactiveExportMax_VARh = reactiveExportMax,
      ReactiveExportAmount_VARh = reactiveExportAmount,
      ActiveImportMin_Wh = activeImportMin,
      ActiveImportMax_Wh = activeImportMax,
      ActiveImportAmount_Wh = activeImportAmount,
      Price_EUR = price,
      Amount_VARh = amount,
      Total_EUR = total
    };
  }
}
