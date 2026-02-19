using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseDivideTest
{
  public static IEnumerable<(
    PhasicMeasure<decimal>,
    decimal,
    PhasicMeasure<decimal>
  )> PhasicMeasuresDivide()
  {
    return new List<(PhasicMeasure<decimal>, decimal, PhasicMeasure<decimal>)>
    {
      (
        new SinglePhasicSumMeasure<decimal>(10),
        2,
        new SinglePhasicSumMeasure<decimal>(5)
      ),
      (
        new TriPhasicMeasure<decimal>(9, 6, 3),
        3,
        new TriPhasicMeasure<decimal>(3, 2, 1)
      ),
      (new NullPhasicMeasure<decimal>(), 2, new NullPhasicMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresDivide))]
  public void DivideReturnsExpectedResult(
    PhasicMeasure<decimal> measure,
    decimal divisor,
    PhasicMeasure<decimal> expected
  )
  {
    var result = measure.Divide(divisor);

    result.Should().Be(expected);
  }
}
