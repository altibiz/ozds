using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSumTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSumSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(6),
      new TriPhasicMeasure<decimal>(1, 2, 3)
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresSumZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(0),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSumSix))]
  public void ReturnsSumSix(PhasicMeasure<decimal> x)
  {
    x.PhaseSum().Should().Be(6);
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSumZero))]
  public void SumZeroReturnsZero(PhasicMeasure<decimal> x)
  {
    x.PhaseSum().Should().Be(0);
  }
}
