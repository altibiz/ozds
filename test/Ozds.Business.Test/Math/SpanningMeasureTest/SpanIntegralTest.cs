using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanIntegralTest
{
  public static readonly TheoryData<SpanningMeasure<decimal>, decimal, TariffMeasure<decimal>> SpanningMeasuresIntegral = new()
  {
    { new MinMaxSpanningMeasure<decimal>(new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(5), new SinglePhasicMeasure<decimal>(3))),
                                          new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(10), new SinglePhasicMeasure<decimal>(6)))),
      2,
      new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(5), new SinglePhasicMeasure<decimal>(3))) },

    { new AvgSpanningMeasure<decimal>(new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3), new TriPhasicMeasure<decimal>(4, 5, 6)))),
      3,
      new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(3, 6, 9), new TriPhasicMeasure<decimal>(12, 15, 18))) },

    { new NullSpanningMeasure<decimal>(), 2, new NullTariffMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(SpanningMeasuresIntegral))]
  public void SpanIntegral_ReturnsExpectedResult(SpanningMeasure<decimal> measure, decimal y, TariffMeasure<decimal> expected)
  {
    var result = measure.SpanIntegral(y);

    Assert.Equal(expected, result);
  }
}
