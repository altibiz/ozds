using Ozds.Business.Finance.Base;
using Ozds.Business.Math;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class UsageActiveEnergyTotalImportT2CalculationItemCalculator :
  CalculationItemCalculator<UsageActiveEnergyTotalImportT2CalculationItemModel>
{
  protected override UsageActiveEnergyTotalImportT2CalculationItemModel
    CalculateConcrete(CalculationItemBasisModel calculationBasis)
  {
    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var amount = new MinMaxSpanningMeasure<decimal>(
      aggregates.FirstOrDefault()!.ActiveEnergy_Wh
        .ConvertPrimitiveTo<decimal>(),
      aggregates.LastOrDefault()!.ActiveEnergy_Wh.ConvertPrimitiveTo<decimal>()
    );

    return new UsageActiveEnergyTotalImportT2CalculationItemModel
    {
      Min_Wh = amount.SpanMin.TariffBinary.T2.DuplexImport.PhaseSum,
      Max_Wh = amount.SpanMax.TariffBinary.T2.DuplexImport.PhaseSum,
      Amount_Wh = amount.SpanDiff.TariffBinary.T2.DuplexImport.PhaseSum,
      Price_EUR = calculationBasis.Price,
      Total_EUR = amount.SpanDiff.TariffBinary.T2.DuplexImport.PhaseSum
                  * calculationBasis.Price
    };
  }
}
