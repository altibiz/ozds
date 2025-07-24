using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseMultiplyByMeasureTest
{
  public static IEnumerable<(PhasicMeasure<decimal>, PhasicMeasure<decimal>,
    PhasicMeasure<decimal>)> PhasicMeasuresMultiply()
  {
    return new List<(PhasicMeasure<decimal>, PhasicMeasure<decimal>,
      PhasicMeasure<decimal>)>
    {
      (new SinglePhasicSumMeasure<decimal>(5),
        new SinglePhasicSumMeasure<decimal>(3),
        new SinglePhasicSumMeasure<decimal>(15)
      ),

      (new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(4, 5, 6),
        new TriPhasicMeasure<decimal>(4, 10, 18)
      ),

      (new NullPhasicMeasure<decimal>(),
        new SinglePhasicSumMeasure<decimal>(2),
        new NullPhasicMeasure<decimal>()
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected)
  {
    var result = lhs.Multiply(rhs);

    result.Should().Be(expected);
  }
}
