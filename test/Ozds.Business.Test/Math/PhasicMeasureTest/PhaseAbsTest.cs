using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAbsTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAbsSinglePhaseSix = new()
    {
      new SinglePhasicMeasureSum<decimal>(6)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAbsTriPhaseSix = new()
    {
      new TriPhasicMeasure<decimal>(-6, 6, 6),
      new TriPhasicMeasure<decimal>(-6, -6, -6)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAbsSinglePhaseZero = new()
    {
      new SinglePhasicMeasureSum<decimal>(0)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAbsTriPhaseZero = new()
    {
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new TriPhasicMeasure<decimal>(0, -0, 0)
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresAbsSinglePhaseSix))]
  public void ReturnsAbsSinglePhaseSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new SinglePhasicMeasureSum<decimal>(6), x.PhaseAbs());
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresAbsTriPhaseSix))]
  public void ReturnsAbsTriPhaseSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new TriPhasicMeasure<decimal>(6, 6, 6), x.PhaseAbs());
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresAbsSinglePhaseZero))]
  public void ReturnsAbsSinglePhaseZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new SinglePhasicMeasureSum<decimal>(0), x.PhaseAbs());
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresAbsTriPhaseZero))]
  public void ReturnsAbsTriPhaseZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(new TriPhasicMeasure<decimal>(0, 0, 0), x.PhaseAbs());
  }
}
