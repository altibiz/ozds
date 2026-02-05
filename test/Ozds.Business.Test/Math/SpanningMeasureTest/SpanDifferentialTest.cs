using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanDifferentialTest
{
  public static IEnumerable<(
    SpanningMeasure<decimal>,
    decimal,
    TariffMeasure<decimal>
  )> SpanningMeasuresDifferential()
  {
    return new List<(SpanningMeasure<decimal>, decimal, TariffMeasure<decimal>)>
    {
      (
        new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(5),
              new SinglePhasicSumMeasure<decimal>(3)
            )
          ),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(10),
              new SinglePhasicSumMeasure<decimal>(6)
            )
          )
        ),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2.5m),
            new SinglePhasicSumMeasure<decimal>(1.5m)
          )
        )
      ),
      (
        new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6)
            )
          ),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(7, 8, 9),
              new TriPhasicMeasure<decimal>(10, 11, 12)
            )
          )
        ),
        3,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(2, 2, 2),
            new TriPhasicMeasure<decimal>(2, 2, 2)
          )
        )
      ),
      (new NullSpanningMeasure<decimal>(), 2, new NullTariffMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(SpanningMeasuresDifferential))]
  public void SpanDifferential_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure,
    decimal y,
    TariffMeasure<decimal> expected
  )
  {
    var result = measure.SpanDifferential(y);

    result.Should().Be(expected);
  }
}
