using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexConvertPrimitiveToFloatTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, DuplexMeasure<float>>
    DuplexMeasuresConvertToFloat = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new ImportExportDuplexMeasure<float>(
          new SinglePhasicMeasureSum<float>(5f),
          new SinglePhasicMeasureSum<float>(3f))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new NetDuplexMeasure<float>(new SinglePhasicMeasureSum<float>(4f))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new AnyDuplexMeasure<float>(new SinglePhasicMeasureSum<float>(3f))
      },

      { new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<float>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloatReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    DuplexMeasure<float> expected)
  {
    var result = measure.ConvertPrimitiveTo<float>();

    Assert.Equal(expected, result);
  }
}
