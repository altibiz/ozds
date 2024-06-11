using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAverageTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresAverageSix = new()
  {
    { new SinglePhasicMeasure<decimal>(6) },
    { new TriPhasicMeasure<decimal>(12, 0, 6) },
    { new TriPhasicMeasure<decimal>(24, -12, 6) }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresAverageSix))]
  public void ReturnsAverageSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseAverage);
  }

  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresAverageZero = new()
  {
    { new SinglePhasicMeasure<decimal>(0) },
    { new TriPhasicMeasure<decimal>(0, 0, 0) },
    { new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresAverageZero))]
  public void ReturnsAverageZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseAverage);
  }
}
