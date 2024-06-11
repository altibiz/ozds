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
    var aggregates = calculationBasis.Aggregates
      .OrderBy(a => a.Timestamp)
      .ToList();

    var reactiveAmount = new MinMaxSpanningMeasure<decimal>(
      aggregates.FirstOrDefault()!.ReactiveEnergy_VARh
        .ConvertPrimitiveTo<decimal>(),
      aggregates.LastOrDefault()!.ReactiveEnergy_VARh
        .ConvertPrimitiveTo<decimal>()
    );

    var activeAmount = new MinMaxSpanningMeasure<decimal>(
      aggregates.FirstOrDefault()!.ActiveEnergy_Wh
        .ConvertPrimitiveTo<decimal>(),
      aggregates.LastOrDefault()!.ActiveEnergy_Wh.ConvertPrimitiveTo<decimal>()
    );

    var rampedAmount =
      reactiveAmount.SpanDiff.Select(duplex =>
        new AnyDuplexMeasure<decimal>(duplex.DuplexAbs.DuplexSum)).Reduce(
      activeAmount.SpanDiff.Select(duplex =>
        new AnyDuplexMeasure<decimal>(duplex.DuplexAny)));

    rampedAmount = rampedAmount
      .Select(duplex => duplex.DuplexAny.PhaseSum < 0
        ? DuplexMeasure<decimal>.Null
        : duplex);

    return new UsageReactiveEnergyTotalRampedT0CalculationItemModel
    {
      ImportMin_VARh = reactiveAmount.SpanMin.TariffUnary.DuplexImport.PhaseSum,
      ImportMax_VARh = reactiveAmount.SpanMax.TariffUnary.DuplexImport.PhaseSum,
      ImportAmount_VARh =
        reactiveAmount.SpanDiff.TariffUnary.DuplexImport.PhaseSum,
      ExportMin_VARh = reactiveAmount.SpanMin.TariffUnary.DuplexExport.PhaseSum,
      ExportMax_VARh = reactiveAmount.SpanMax.TariffUnary.DuplexExport.PhaseSum,
      ExportAmount_VARh =
        reactiveAmount.SpanDiff.TariffUnary.DuplexExport.PhaseSum,
      Price_EUR = calculationBasis.Price,
      Amount_VARh = rampedAmount.TariffUnary.DuplexAny.PhaseSum,
      Total_EUR = rampedAmount.TariffUnary.DuplexAny.PhaseSum *
                  calculationBasis.Price
    };
  }
}
