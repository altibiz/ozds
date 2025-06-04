using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSingleTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSingleSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicMeasureSum<decimal>(6),
      new CompositePhasicMeasure<decimal>(
      [
        new SinglePhasicMeasureSum<decimal>(6),
        new TriPhasicMeasure<decimal>(1, 1, 1)
      ])
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSingleZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicMeasureSum<decimal>(0),
      new CompositePhasicMeasure<decimal>(
        [new TriPhasicMeasure<decimal>(1, 1, 1)]),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSingleSix))]
  public void ReturnsSingleSix(PhasicMeasure<decimal> x)
  {
    x.PhaseSingle().Value.Should().Be(6);
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSingleZero))]
  public void ReturnsSingleZero(PhasicMeasure<decimal> x)
  {
    x.PhaseSingle().Value.Should().Be(0);
  }
}
