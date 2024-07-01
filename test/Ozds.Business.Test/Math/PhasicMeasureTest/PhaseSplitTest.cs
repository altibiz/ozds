using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSplitTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresSplitSix = new()
    {
      new SinglePhasicMeasure<decimal>(6),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasure<decimal>(6),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      ]),
      new TriPhasicMeasure<decimal>(6, 6, 6)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresSplitZero = new()
    {
      new SinglePhasicMeasure<decimal>(0),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasure<decimal>(0),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      ]),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSplitSix))]
  public void ReturnsSplitSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new TriPhasicMeasure<decimal>(6, 6, 6), x.PhaseSplit);
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresSplitZero))]
  public void ReturnsSplitZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new TriPhasicMeasure<decimal>(0, 0, 0), x.PhaseSplit);
  }
}
