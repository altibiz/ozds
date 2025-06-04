using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSplitTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSplitSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicMeasureSum<decimal>(18),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasureSum<decimal>(3),
        new TriPhasicMeasure<decimal>(6, 6, 6)
      ]),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasureSum<decimal>(18),
        new TriPhasicMeasure<decimal>(0, 0, 0)
      ]),
      new TriPhasicMeasure<decimal>(6, 6, 6)
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSplitZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicMeasureSum<decimal>(0),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasureSum<decimal>(0),
        new TriPhasicMeasure<decimal>(0, 0, 0)
      ]),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSplitSix))]
  public void ReturnsSplitSix(PhasicMeasure<decimal> x)
  {
    x.PhaseSplit().Should()
      .BeEquivalentTo(new TriPhasicMeasure<decimal>(6, 6, 6));
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSplitZero))]
  public void ReturnsSplitZero(PhasicMeasure<decimal> x)
  {
    x.PhaseSplit().Should()
      .BeEquivalentTo(new TriPhasicMeasure<decimal>(0, 0, 0));
  }
}
