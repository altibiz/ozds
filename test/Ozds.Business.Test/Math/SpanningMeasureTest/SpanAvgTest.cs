using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class SpanAvgTest
{
  public static IEnumerable<(SpanningMeasure<decimal>, TariffMeasure<decimal>)>
    SpanningMeasuresAvg()
  {
    return new List<(SpanningMeasure<decimal>, TariffMeasure<decimal>)>
    {
      (new AvgSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(5),
              new SinglePhasicSumMeasure<decimal>(3)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)))
      ),

      (new AvgSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)))
      ),

      (new NullSpanningMeasure<decimal>(), new NullTariffMeasure<decimal>())
    };
  }

  [Test]
  [MethodDataSource(nameof(SpanningMeasuresAvg))]
  public void SpanAvg_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure,
    TariffMeasure<decimal> expected)
  {
    var result = measure.SpanAvg();

    result.Should().Be(expected);
  }
}
