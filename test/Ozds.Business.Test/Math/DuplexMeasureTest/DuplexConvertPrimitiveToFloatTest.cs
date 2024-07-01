using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexConvertPrimitiveToFloatTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, DuplexMeasure<float>>
    DuplexMeasuresConvertToFloat = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3)),
        new ImportExportDuplexMeasure<float>(
          new SinglePhasicMeasure<float>(5f),
          new SinglePhasicMeasure<float>(3f))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(4)),
        new NetDuplexMeasure<float>(new SinglePhasicMeasure<float>(4f))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(3)),
        new AnyDuplexMeasure<float>(new SinglePhasicMeasure<float>(3f))
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
