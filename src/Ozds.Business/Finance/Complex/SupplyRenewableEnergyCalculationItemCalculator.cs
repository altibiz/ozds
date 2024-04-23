using Ozds.Business.Finance.Base;
using Ozds.Business.Math;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance.Complex;

public class SupplyRenewableEnergyCalculationItemCalculator :
  CalculationItemCalculator<SupplyRenewableEnergyCalculationItemModel>
{
  protected override SupplyRenewableEnergyCalculationItemModel
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

    return new SupplyRenewableEnergyCalculationItemModel
    {
      Min_Wh = amount.SpanMin.TariffUnary.DuplexImport.PhaseSum,
      Max_Wh = amount.SpanMax.TariffUnary.DuplexImport.PhaseSum,
      Amount_Wh = amount.SpanDiff.TariffUnary.DuplexImport.PhaseSum,
      Price_EUR = calculationBasis.Price,
      Total_EUR = amount.SpanDiff.TariffUnary.DuplexImport.PhaseSum
                  * calculationBasis.Price
    };
  }
}