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
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new UsageActivePowerTotalImportT1PeakCalculationItemModel
      {
        Peak_W = 0,
        Amount_W = 0,
        Price_EUR = calculationBasis.Price,
        Total_EUR = 0
      };
    }

    var aggregates = calculationBasis.Aggregates;

    var peakOriginal = aggregates
      .Select(x => x.ActivePower_W
        .TariffBinary().T1
        .DuplexImport()
        .PhaseSum())
      .Max();

    var peak = System.Math.Round(peakOriginal, 2);

    var amount = System.Math.Round(peak, 0);

    var price = System.Math.Round(calculationBasis.Price, 3);

    var total = System.Math.Round(amount * price, 2);

    return new UsageActivePowerTotalImportT1PeakCalculationItemModel
    {
      Peak_W = peak,
      Amount_W = amount,
      Price_EUR = price,
      Total_EUR = total,
    };
  }
}
