using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhasePeakTest
{
  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresPeakSix()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(18),
      new TriPhasicMeasure<decimal>(1, 5.9m, 6),
      new TriPhasicMeasure<decimal>(0, -12, 6),
    };
  }

  public static IEnumerable<PhasicMeasure<decimal>> PhasicMeasuresPeakZero()
  {
    return new List<PhasicMeasure<decimal>>
    {
      new SinglePhasicSumMeasure<decimal>(0),
      new TriPhasicMeasure<decimal>(0, 0, 0),
      new NullPhasicMeasure<decimal>(),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresPeakSix))]
  public void ReturnsPeakSix(PhasicMeasure<decimal> x)
  {
    x.PhasePeak().Should().Be(6);
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresPeakZero))]
  public void ReturnsPeakZero(PhasicMeasure<decimal> x)
  {
    x.PhasePeak().Should().Be(0);
  }
}
