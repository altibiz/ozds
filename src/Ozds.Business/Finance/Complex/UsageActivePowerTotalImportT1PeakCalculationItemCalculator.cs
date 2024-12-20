using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageActivePowerTotalImportT1PeakCalculationItemCalculator :
  CalculationItemCalculator<
    UsageActivePowerTotalImportT1PeakCalculationItemModel>
{
  protected override UsageActivePowerTotalImportT1PeakCalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    if (calculationBasis.Aggregates.Count <= 1)
    {
      return new UsageActivePowerTotalImportT1PeakCalculationItemModel
      {
        Peak_kW = 0,
        Amount_kW = 0,
        Price_EUR = calculationBasis.Price,
        Total_EUR = 0
      };
    }

    var aggregates = calculationBasis.Aggregates;

    var peak = aggregates
      .SkipLast(1)
      .Select(
        x => x.DerivedActivePower_W
          .TariffBinary().T1
          .DuplexImport()
          .AggregateMax()
          .PhaseSum())
      .Max();

    var peakKilo = System.Math.Round(peak / 1000M, 2);

    var amountKilo = System.Math.Round(peakKilo, 0);

    var price = System.Math.Round(calculationBasis.Price, 3);

    var total = System.Math.Round(amountKilo * price, 2);

    return new UsageActivePowerTotalImportT1PeakCalculationItemModel
    {
      Peak_kW = peakKilo,
      Amount_kW = amountKilo,
      Price_EUR = price,
      Total_EUR = total
    };
  }
}
