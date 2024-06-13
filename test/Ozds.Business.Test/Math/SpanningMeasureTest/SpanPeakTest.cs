using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanPeakTest
{
  public static readonly TheoryData<SpanningMeasure<decimal>, TariffMeasure<decimal>> SpanningMeasuresPeak = new()
  {
    { new PeakSpanningMeasure<decimal>(new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(5), new SinglePhasicMeasure<decimal>(3)))),
      new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(5), new SinglePhasicMeasure<decimal>(3))) },

    { new PeakSpanningMeasure<decimal>(new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3), new TriPhasicMeasure<decimal>(4, 5, 6)))),
      new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3), new TriPhasicMeasure<decimal>(4, 5, 6))) },

    { new NullSpanningMeasure<decimal>(), new NullTariffMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(SpanningMeasuresPeak))]
  public void SpanPeak_ReturnsExpectedResult(SpanningMeasure<decimal> measure, TariffMeasure<decimal> expected)
  {
    var result = measure.SpanPeak;

    Assert.Equal(expected, result);
  }
}
