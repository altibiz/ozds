using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseDivideTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>, decimal, PhasicMeasure<decimal>> PhasicMeasuresDivide = new()
  {
    { new SinglePhasicMeasure<decimal>(10), 2, new SinglePhasicMeasure<decimal>(5) },

    { new TriPhasicMeasure<decimal>(9, 6, 3), 3, new TriPhasicMeasure<decimal>(3, 2, 1) },

    { new NullPhasicMeasure<decimal>(), 2, new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresDivide))]
  public void DivideReturnsExpectedResult(PhasicMeasure<decimal> measure, decimal divisor, PhasicMeasure<decimal> expected)
  {
    var result = measure.Divide(divisor);

    Assert.Equal(expected, result);
  }
}
