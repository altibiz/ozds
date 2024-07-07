using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseMultiplyByMeasureTest
{
  public static readonly
    TheoryData<PhasicMeasure<decimal>, PhasicMeasure<decimal>,
      PhasicMeasure<decimal>> PhasicMeasuresMultiply = new()
    {
      {
        new SinglePhasicMeasureSum<decimal>(5),
        new SinglePhasicMeasureSum<decimal>(3),
        new SinglePhasicMeasureSum<decimal>(15)
      },

      {
        new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(4, 5, 6),
        new TriPhasicMeasure<decimal>(4, 10, 18)
      },

      {
        new NullPhasicMeasure<decimal>(), new SinglePhasicMeasureSum<decimal>(2),
        new NullPhasicMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected)
  {
    var result = lhs.Multiply(rhs);

    Assert.Equal(expected, result);
  }
}
