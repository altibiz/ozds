using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSubtractTest
{
  public static readonly
    TheoryData<PhasicMeasure<decimal>, PhasicMeasure<decimal>,
      PhasicMeasure<decimal>> PhasicMeasuresSubtract = new()
    {
      {
        new SinglePhasicMeasureSum<decimal>(8),
        new SinglePhasicMeasureSum<decimal>(3),
        new SinglePhasicMeasureSum<decimal>(5)
      },

      {
        new TriPhasicMeasure<decimal>(10, 7, 4),
        new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(9, 5, 1)
      },

      {
        new NullPhasicMeasure<decimal>(),
        new SinglePhasicMeasureSum<decimal>(2),
        new NullPhasicMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSubtract))]
  public void SubtractReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected)
  {
    var result = lhs.Subtract(rhs);

    Assert.Equal(expected, result);
  }
}
