using Ozds.Business.Finance.Base;
using Ozds.Business.Math;
using Ozds.Business.Models.Complex;
using Ozds.Business.Models.Composite;

// TODO: remove exclamations from these calculations

namespace Ozds.Business.Finance.Complex;

public class UsageActiveEnergyTotalImportT1CalculationItemCalculator :
  CalculationItemCalculator<UsageActiveEnergyTotalImportT1CalculationItemModel>
{
  protected override UsageActiveEnergyTotalImportT1CalculationItemModel
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

    return new UsageActiveEnergyTotalImportT1CalculationItemModel
    {
      Min_Wh = amount.SpanMin().TariffBinary().T1.DuplexImport().PhaseSum(),
      Max_Wh = amount.SpanMax().TariffBinary().T1.DuplexImport().PhaseSum(),
      Amount_Wh = amount.SpanDiff().TariffBinary().T1.DuplexImport().PhaseSum(),
      Price_EUR = calculationBasis.Price,
      Total_EUR = amount.SpanDiff().TariffBinary().T1.DuplexImport().PhaseSum()
        * calculationBasis.Price
    };
  }
}
