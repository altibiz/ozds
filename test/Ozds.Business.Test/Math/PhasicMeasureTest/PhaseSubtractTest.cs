using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseSubtractTest
{
  public static IEnumerable<(PhasicMeasure<decimal>, PhasicMeasure<decimal>,
    PhasicMeasure<decimal>)> PhasicMeasuresSubtract()
  {
    return new List<(PhasicMeasure<decimal>, PhasicMeasure<decimal>,
      PhasicMeasure<decimal>)>
    {
      (new SinglePhasicSumMeasure<decimal>(8),
        new SinglePhasicSumMeasure<decimal>(3),
        new SinglePhasicSumMeasure<decimal>(5)
      ),

      (new TriPhasicMeasure<decimal>(10, 7, 4),
        new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(9, 5, 1)
      ),

      (new NullPhasicMeasure<decimal>(),
        new SinglePhasicSumMeasure<decimal>(2),
        new NullPhasicMeasure<decimal>()
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresSubtract))]
  public void SubtractReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected)
  {
    var result = lhs.Subtract(rhs);

    result.Should().Be(expected);
  }
}
