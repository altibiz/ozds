using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAverageTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresAverageSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new TriPhasicMeasure<decimal>(12, 0, 6),
      new TriPhasicMeasure<decimal>(24, -12, 6),
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresAverageZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(0),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>(),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAverageSix))]
  public void ReturnsAverageSix(PhasicMeasure<decimal> x)
  {
    x.PhaseAverage().Should().Be(6);
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAverageZero))]
  public void ReturnsAverageZero(PhasicMeasure<decimal> x)
  {
    x.PhaseAverage().Should().Be(0);
  }
}
