using Ozds.Business.Finance.Base;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;
using static Ozds.Business.Extensions.PrimitiveRoundingExtensions;

namespace Ozds.Business.Finance.Complex;

public class UsageActiveEnergyTotalImportT2CalculationItemCalculator
  : CalculationItemCalculator<UsageActiveEnergyTotalImportT2CalculationItemModel>
{
  protected override UsageActiveEnergyTotalImportT2CalculationItemModel CalculateConcrete(
    CalculationItemBasisModel calculationBasis
  )
  {
    if (calculationBasis.Aggregates.Count == 0)
    {
      return new UsageActiveEnergyTotalImportT2CalculationItemModel
      {
        Min_kWh = 0,
        Max_kWh = 0,
        Amount_kWh = 0,
        Price_EUR = calculationBasis.Price_EUR,
        Total_EUR = 0,
      };
    }

    var aggregates = calculationBasis
      .Aggregates.OrderBy(a => a.Timestamp)
      .ToList();

    var min = aggregates
      .First()
      .ActiveEnergy_Wh.TariffBinary()
      .T2.DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var minKilo = Round(min / 1000M, 2);

    var max = aggregates
      .Last()
      .ActiveEnergy_Wh.TariffBinary()
      .T2.DuplexImport()
      .AggregateMin()
      .PhaseSum();

    var maxKilo = Round(max / 1000M, 2);

    var amountKilo = Round(maxKilo - minKilo, 0);

    var price = Round(calculationBasis.Price_EUR, 6);

    var total = Round(amountKilo * price, 2);

    return new UsageActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_kWh = minKilo,
      Max_kWh = maxKilo,
      Amount_kWh = amountKilo,
      Price_EUR = price,
      Total_EUR = total,
    };
  }
}
