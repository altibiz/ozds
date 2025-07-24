using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAbsTest
{
  public static IEnumerable<PhasicMeasure<decimal>>
    PhasicMeasuresAbsSinglePhaseSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(6)
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>>
    PhasicMeasuresAbsTriPhaseSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new TriPhasicMeasure<decimal>(-6, 6, 6),
      new TriPhasicMeasure<decimal>(-6, -6, -6)
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>>
    PhasicMeasuresAbsSinglePhaseZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(0)
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>>
    PhasicMeasuresAbsTriPhaseZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new TriPhasicMeasure<decimal>(0, -0, 0)
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAbsSinglePhaseSix))]
  public void ReturnsAbsSinglePhaseSix(PhasicMeasure<decimal> x)
  {
    x.PhaseAbs().Should()
      .BeEquivalentTo(new SinglePhasicSumMeasure<decimal>(6));
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAbsTriPhaseSix))]
  public void ReturnsAbsTriPhaseSix(PhasicMeasure<decimal> x)
  {
    x.PhaseAbs().Should()
      .BeEquivalentTo(new TriPhasicMeasure<decimal>(6, 6, 6));
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAbsSinglePhaseZero))]
  public void ReturnsAbsSinglePhaseZero(PhasicMeasure<decimal> x)
  {
    x.PhaseAbs().Should()
      .BeEquivalentTo(new SinglePhasicSumMeasure<decimal>(0));
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAbsTriPhaseZero))]
  public void ReturnsAbsTriPhaseZero(PhasicMeasure<decimal> x)
  {
    x.PhaseAbs().Should()
      .BeEquivalentTo(new TriPhasicMeasure<decimal>(0, 0, 0));
  }
}
