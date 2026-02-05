using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexConvertPrimitiveToFloatTest
{
  public static IEnumerable<(
    DuplexMeasure<decimal>,
    DuplexMeasure<float>
  )> DuplexMeasuresConvertToFloat()
  {
    return new List<(DuplexMeasure<decimal>, DuplexMeasure<float>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)
        ),
        new ImportExportDuplexMeasure<float>(
          new SinglePhasicSumMeasure<float>(5f),
          new SinglePhasicSumMeasure<float>(3f)
        )
      ),
      (
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4)),
        new NetDuplexMeasure<float>(new SinglePhasicSumMeasure<float>(4f))
      ),
      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(3)),
        new AnyDuplexMeasure<float>(new SinglePhasicSumMeasure<float>(3f))
      ),
      (new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<float>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresConvertToFloat))]
  public void ConvertPrimitiveToFloatReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    DuplexMeasure<float> expected
  )
  {
    var result = measure.ConvertPrimitiveTo<float>();

    result.Should().Be(expected);
  }
}
