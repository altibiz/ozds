using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class SupplyActiveEnergyTotalImportT2CalculationItemCalculator :
  CalculationItemCalculator<SupplyActiveEnergyTotalImportT2CalculationItemModel>
{
  protected override SupplyActiveEnergyTotalImportT2CalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new SupplyActiveEnergyTotalImportT2CalculationItemModel
      {
        Min_Wh = 0,
        Max_Wh = 0,
        Amount_Wh = 0,
        Price_EUR = calculationBasis.Price,
        Total_EUR = 0
      };
    }

    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var minOriginal = aggregates
        .First().ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .PhaseSum();

    var min = System.Math.Round(minOriginal, 2);

    var maxOriginal = aggregates
        .Last().ActiveEnergy_Wh
        .TariffBinary().T2
        .DuplexImport()
        .PhaseSum();

    var max = System.Math.Round(maxOriginal, 2);

    var amount = System.Math.Round(max - min, 0);

    var price = System.Math.Round(calculationBasis.Price, 6);

    var total = System.Math.Round(amount * price, 2);

    return new SupplyActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_Wh = min,
      Max_Wh = max,
      Amount_Wh = amount,
      Price_EUR = price,
      Total_EUR = total
    };
  }
}
