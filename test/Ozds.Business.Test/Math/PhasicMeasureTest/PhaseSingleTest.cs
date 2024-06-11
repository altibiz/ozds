using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSingleTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresSingleSix = new()
  {
    { new SinglePhasicMeasure<decimal>(6) },
    { new CompositePhasicMeasure<decimal>(
      new List<PhasicMeasure<decimal>> { new SinglePhasicMeasure<decimal>(6), new TriPhasicMeasure<decimal>(1, 1, 1) })
    }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSingleSix))]
  public void ReturnsSingleSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseSingle.Value);
  }

  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresSingleZero = new()
  {
    { new SinglePhasicMeasure<decimal>(0) },
    { new CompositePhasicMeasure<decimal>(
      new List<PhasicMeasure<decimal>> {  new TriPhasicMeasure<decimal>(1, 1, 1) })
    },
    { new TriPhasicMeasure<decimal>(0, 0, 0) },
    { new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresSingleZero))]
  public void ReturnsSingleZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseSingle.Value);
  }
}
