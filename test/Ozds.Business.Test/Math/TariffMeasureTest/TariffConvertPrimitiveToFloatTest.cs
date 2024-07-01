using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffConvertPrimitiveToFloatTest
{
  public static readonly
    TheoryData<TariffMeasure<decimal>, TariffMeasure<float>>
    TariffMeasuresConvertToFloat = new()
    {
      {
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(5),
            new SinglePhasicMeasure<decimal>(3))),
        new UnaryTariffMeasure<float>(
          new ImportExportDuplexMeasure<float>(
            new SinglePhasicMeasure<float>(5f),
            new SinglePhasicMeasure<float>(3f)))
      },

      {
        new BinaryTariffMeasure<decimal>(
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
      },

      { new NullTariffMeasure<decimal>(), new NullTariffMeasure<float>() }
    };

  [Theory]
  [MemberData(nameof(TariffMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloat_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    TariffMeasure<float> expected)
  {
    var result = measure.ConvertPrimitiveTo<float>();

    Assert.Equal(expected, result);
  }
}
