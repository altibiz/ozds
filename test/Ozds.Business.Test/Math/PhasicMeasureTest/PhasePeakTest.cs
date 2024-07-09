using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhasePeakTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresPeakSix = new()
    {
      new TriPhasicMeasure<decimal>(1, 5.9m, 6),
      new TriPhasicMeasure<decimal>(0, -12, 6)
    };

  public static readonly TheoryData<PhasicMeasure<decimal>>
    PhasicMeasuresPeakZero = new()
    {
      new SinglePhasicMeasureSum<decimal>(0),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>()
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresPeakSix))]
  public void ReturnsPeakSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhasePeak());
  }

  [Theory]
  [MemberData(nameof(PhasicMeasuresPeakZero))]
  public void ReturnsPeakZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhasePeak());
  }
}
