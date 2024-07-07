using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSingleTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresSingleSix = new()
    {
      new SinglePhasicMeasureSum<decimal>(6),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasureSum<decimal>(6),
        new TriPhasicMeasure<decimal>(1, 1, 1)
      ])
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresSingleZero = new()
    {
      new SinglePhasicMeasureSum<decimal>(0),
      new CompositePhasicMeasure<decimal>(
        [new TriPhasicMeasure<decimal>(1, 1, 1)]),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSingleSix))]
  public void ReturnsSingleSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseSingle.Value);
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresSingleZero))]
  public void ReturnsSingleZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseSingle.Value);
  }
}
