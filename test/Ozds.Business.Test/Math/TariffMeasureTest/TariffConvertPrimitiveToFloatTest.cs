using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffConvertPrimitiveToFloatTest
{
  public static IEnumerable<(TariffMeasure<decimal>, TariffMeasure<float>)>
    TariffMeasuresConvertToFloat()
  {
    return new List<(TariffMeasure<decimal>, TariffMeasure<float>)>
    {
      (new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3))),
        new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new SinglePhasicSumMeasure<float>(5f),
            new SinglePhasicSumMeasure<float>(3f)))
      ),

      (new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)),
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(7, 8, 9),
            new TriPhasicMeasure<decimal>(10, 11, 12))),
        new BinaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new TriPhasicMeasure<float>(1f, 2f, 3f),
            new TriPhasicMeasure<float>(4f, 5f, 6f)),
          new ImportExportDuplexMeasure<float>(
            new TriPhasicMeasure<float>(7f, 8f, 9f),
            new TriPhasicMeasure<float>(10f, 11f, 12f)))
      ),

      (new NullTariffMeasure<decimal>(), new NullTariffMeasure<float>())
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloat_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    TariffMeasure<float> expected)
  {
    var result = measure.ConvertPrimitiveTo<float>();

    result.Should().Be(expected);
  }
}
