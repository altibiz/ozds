using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAddTest
{
  public static readonly
    TheoryData<PhasicMeasure<decimal>, PhasicMeasure<decimal>,
      PhasicMeasure<decimal>> PhasicMeasuresAdd = new()
    {
      {
        new SinglePhasicMeasure<decimal>(5),
        new SinglePhasicMeasure<decimal>(3), new SinglePhasicMeasure<decimal>(8)
      },

      {
        new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(4, 5, 6),
        new TriPhasicMeasure<decimal>(5, 7, 9)
      },

      {
        new NullPhasicMeasure<decimal>(), new SinglePhasicMeasure<decimal>(2),
        new NullPhasicMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresAdd))]
  public void AddReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected)
  {
    var result = lhs.Add(rhs);

    Assert.Equal(expected, result);
  }
}
