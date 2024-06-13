using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanDifferentialTest
{
  public static readonly
    TheoryData<SpanningMeasure<decimal>, decimal, TariffMeasure<decimal>>
    SpanningMeasuresDifferential = new()
    {
      {
        new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(5),
              new SinglePhasicMeasure<decimal>(3))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(10),
              new SinglePhasicMeasure<decimal>(6)))),
        2,
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(2.5m),
          new SinglePhasicMeasure<decimal>(1.5m)))
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
        3,
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 2, 2),
          new TriPhasicMeasure<decimal>(2, 2, 2)))
      },

      {
        new NullSpanningMeasure<decimal>(), 2, new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(SpanningMeasuresDifferential))]
  public void SpanDifferential_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure, decimal y,
    TariffMeasure<decimal> expected)
  {
    var result = measure.SpanDifferential(y);

    Assert.Equal(expected, result);
  }
}
