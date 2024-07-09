using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanMaxTest
{
  public static readonly
    TheoryData<SpanningMeasure<decimal>, TariffMeasure<decimal>>
    SpanningMeasuresMax = new()
    {
      {
        new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(5),
              new SinglePhasicMeasureSum<decimal>(3))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(10),
              new SinglePhasicMeasureSum<decimal>(6)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(10),
            new SinglePhasicMeasureSum<decimal>(6)))
      },

      {
        new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(7, 8, 9),
              new TriPhasicMeasure<decimal>(10, 11, 12)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(7, 8, 9),
            new TriPhasicMeasure<decimal>(10, 11, 12)))
      },

      { new NullSpanningMeasure<decimal>(), new NullTariffMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(SpanningMeasuresMax))]
  public void SpanMax_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure,
    TariffMeasure<decimal> expected)
  {
    var result = measure.SpanMax();

    Assert.Equal(expected, result);
  }
}
