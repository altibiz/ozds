using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAnyTest
{
  public static readonly TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>> DuplexMeasuresAny = new()
  {
    { new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(4)), new SinglePhasicMeasure<decimal>(4) },
    { new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(3)), new SinglePhasicMeasure<decimal>(3) },
    { new AnyDuplexMeasure<decimal>(new TriPhasicMeasure<decimal>(1, 2, 3)), new TriPhasicMeasure<decimal>(1, 2, 3) },
    { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(DuplexMeasuresAny))]
  public void DuplexAnyReturnsExpectedResult(DuplexMeasure<decimal> measure, PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexAny;

    Assert.Equal(expected, result);
  }
}
