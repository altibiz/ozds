using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseMultiplyTest
{
  public static IEnumerable<(
    PhasicMeasure<decimal>,
    decimal,
    PhasicMeasure<decimal>
  )> PhasicMeasuresMultiply()
  {
    return new List<(PhasicMeasure<decimal>, decimal, PhasicMeasure<decimal>)>
    {
      (
        new SinglePhasicSumMeasure<decimal>(5),
        2,
        new SinglePhasicSumMeasure<decimal>(10)
      ),
      (
        new TriPhasicMeasure<decimal>(1, 2, 3),
        3,
        new TriPhasicMeasure<decimal>(3, 6, 9)
      ),
      (new NullPhasicMeasure<decimal>(), 2, new NullPhasicMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    PhasicMeasure<decimal> measure,
    decimal multiplier,
    PhasicMeasure<decimal> expected
  )
  {
    var result = measure.Multiply(multiplier);

    result.Should().Be(expected);
  }
}
