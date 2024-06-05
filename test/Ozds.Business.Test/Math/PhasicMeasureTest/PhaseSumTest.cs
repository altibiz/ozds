using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSumTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresSumSix = new()
  {
    { new SinglePhasicMeasure<decimal>(6) },
    { new TriPhasicMeasure<decimal>(1, 2, 3) }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSumSix))]
  public void ReturnsSumSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseSum);
  }

  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresSumZero = new()
  {
    { new SinglePhasicMeasure<decimal>(0) },
    { new TriPhasicMeasure<decimal>(0, 0, 0) },
    { new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSumZero))]
  public void SumZeroReturnsZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseSum);
  }
}
