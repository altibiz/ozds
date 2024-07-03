using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAverageTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAverageSix = new()
    {
      new SinglePhasicAverage<decimal>(6),
      new TriPhasicMeasure<decimal>(12, 0, 6),
      new TriPhasicMeasure<decimal>(24, -12, 6)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresAverageZero = new()
    {
      new SinglePhasicSum<decimal>(0),
      new SinglePhasicAverage<decimal>(0),
      new SinglePhasicSum<decimal>(6),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresAverageSix))]
  public void ReturnsAverageSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseAverage);
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresAverageZero))]
  public void ReturnsAverageZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseAverage);
  }
}
