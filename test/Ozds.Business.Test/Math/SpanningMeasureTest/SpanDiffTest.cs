using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanDiffTest
{
  public static readonly
    TheoryData<SpanningMeasure<decimal>, TariffMeasure<decimal>>
    SpanningMeasuresDiff = new()
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
            new SinglePhasicMeasureSum<decimal>(5),
            new SinglePhasicMeasureSum<decimal>(3)))
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
            new TriPhasicMeasure<decimal>(6, 6, 6),
            new TriPhasicMeasure<decimal>(6, 6, 6)))
      },

      { new NullSpanningMeasure<decimal>(), new NullTariffMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(SpanningMeasuresDiff))]
  public void SpanDiff_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure,
    TariffMeasure<decimal> expected)
  {
    var result = measure.SpanDiff();

    Assert.Equal(expected, result);
  }
}
