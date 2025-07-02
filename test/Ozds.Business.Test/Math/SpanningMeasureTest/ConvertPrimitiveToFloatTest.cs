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
              new SinglePhasicMeasureSum<decimal>(5),
              new SinglePhasicMeasureSum<decimal>(3))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(10),
              new SinglePhasicMeasureSum<decimal>(6)))),
        new MinMaxSpanningMeasure<float>(
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(5f),
              new SinglePhasicMeasureSum<float>(3f))),
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(10f),
              new SinglePhasicMeasureSum<float>(6f))))
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
              new SinglePhasicMeasureSum<decimal>(7),
              new SinglePhasicMeasureSum<decimal>(8)))),
        new PeakSpanningMeasure<float>(
          new UnaryTariffMeasure<float>(
            new ImportExportDuplexMeasure<float>(
              new SinglePhasicMeasureSum<float>(7f),
              new SinglePhasicMeasureSum<float>(8f))))
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
