using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseTroughTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresTroughSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(18),
      new TriPhasicMeasure<decimal>(100, 6.1m, 6),
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresTroughZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(0),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>(),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresTroughSix))]
  public void ReturnsTroughSix(PhasicMeasure<decimal> x)
  {
    x.PhaseTrough().Should().Be(6);
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresTroughZero))]
  public void ReturnsTroughZero(PhasicMeasure<decimal> x)
  {
    x.PhaseTrough().Should().Be(0);
  }
}
