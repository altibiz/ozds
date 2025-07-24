using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.SpanningMeasureTest;

public class ConvertPrimitiveToFloatTest
{
  public static IEnumerable<(SpanningMeasure<decimal>, SpanningMeasure<float>)>
    SpanningMeasuresConvertToFloat()
  {
    return new List<(SpanningMeasure<decimal>, SpanningMeasure<float>)>
    {
      (new MinMaxSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(5),
              new SinglePhasicSumMeasure<decimal>(3))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(10),
              new SinglePhasicSumMeasure<decimal>(6)))),
        new MinMaxSpanningMeasure<float>(
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicSumMeasure<float>(5f),
              new SinglePhasicSumMeasure<float>(3f))),
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicSumMeasure<float>(10f),
              new SinglePhasicSumMeasure<float>(6f))))
      ),

      (new AvgSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6)))),
        new AvgSpanningMeasure<float>(
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new TriPhasicMeasure<float>(1f, 2f, 3f),
              new TriPhasicMeasure<float>(4f, 5f, 6f))))
      ),

      (new PeakSpanningMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(7),
              new SinglePhasicSumMeasure<decimal>(8)))),
        new PeakSpanningMeasure<float>(
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicSumMeasure<float>(7f),
              new SinglePhasicSumMeasure<float>(8f))))
      ),

      (new NullSpanningMeasure<decimal>(), new NullSpanningMeasure<float>())
    };
  }

  [Test]
  [MethodDataSource(nameof(SpanningMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloat_ReturnsExpectedResult(
    SpanningMeasure<decimal> measure,
    SpanningMeasure<float> expected)
  {
    var result = measure.ConvertPrimitiveTo<float>();

    result.Should().Be(expected);
  }
}
