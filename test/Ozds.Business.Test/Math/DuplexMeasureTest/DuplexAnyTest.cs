using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAnyTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>>
    DuplexMeasuresAny = new()
    {
      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new SinglePhasicMeasureSum<decimal>(4)
      },
      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new SinglePhasicMeasureSum<decimal>(3)
      },
      {
        new AnyDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3)),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      },
      { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresAny))]
  public void DuplexAnyReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexAny;

    Assert.Equal(expected, result);
  }
}
