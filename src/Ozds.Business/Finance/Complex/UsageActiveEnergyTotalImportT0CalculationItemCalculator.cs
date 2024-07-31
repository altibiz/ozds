using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageActiveEnergyTotalImportT0CalculationItemCalculator :
  CalculationItemCalculator<UsageActiveEnergyTotalImportT0CalculationItemModel>
{
  protected override UsageActiveEnergyTotalImportT0CalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new UsageActiveEnergyTotalImportT0CalculationItemModel
      {
        Min_kWh = 0,
        Max_kWh = 0,
        Amount_kWh = 0,
        Price_EUR = calculationBasis.Price,
        Total_EUR = 0
      };
    }

    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var min = aggregates
      .First().ActiveEnergy_Wh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var minKilo = System.Math.Round(min / 1000M, 2);

    var max = aggregates
      .Last().ActiveEnergy_Wh
      .TariffUnary()
      .DuplexImport()
      .PhaseSum();

    var maxKilo = System.Math.Round(max / 1000M, 2);

    var amountKilo = System.Math.Round(maxKilo - minKilo, 0);

    var price = System.Math.Round(calculationBasis.Price, 6);

    var total = System.Math.Round(amountKilo * price, 2);

    return new UsageActiveEnergyTotalImportT0CalculationItemModel
    {
      Min_kWh = minKilo,
      Max_kWh = maxKilo,
      Amount_kWh = amountKilo,
      Price_EUR = price,
      Total_EUR = total
    };
  }
}
