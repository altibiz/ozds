using Ozds.Business.Finance.Base;
using Ozds.Business.Math;
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
    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var amount = new PeakSpanningMeasure<decimal>(
      aggregates
        .MaxBy(
          aggregate =>
            aggregate.ActivePower_W.TariffBinary().T1.DuplexImport().PhaseSum())
        !.ActivePower_W.ConvertPrimitiveTo<decimal>()
    );

    return new UsageActivePowerTotalImportT1PeakCalculationItemModel
    {
      Peak_W = amount.SpanPeak().TariffBinary().T1.DuplexImport().PhaseSum(),
      Amount_W = amount.SpanPeak().TariffBinary().T1.DuplexImport().PhaseSum(),
      Price_EUR = calculationBasis.Price,
      Total_EUR = amount.SpanPeak().TariffBinary().T1.DuplexImport().PhaseSum() *
        calculationBasis.Price
    };
  }
}
