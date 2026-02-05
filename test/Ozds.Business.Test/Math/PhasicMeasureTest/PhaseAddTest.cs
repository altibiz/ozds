using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseAddTest
{
  public static IEnumerable<(
    PhasicMeasure<decimal>,
    PhasicMeasure<decimal>,
    PhasicMeasure<decimal>
  )> PhasicMeasuresAdd()
  {
    return new List<(
      PhasicMeasure<decimal>,
      PhasicMeasure<decimal>,
      PhasicMeasure<decimal>
    )>
    {
      (
        new SinglePhasicSumMeasure<decimal>(5),
        new SinglePhasicSumMeasure<decimal>(3),
        new SinglePhasicSumMeasure<decimal>(8)
      ),
      (
        new TriPhasicMeasure<decimal>(1, 2, 3),
        new TriPhasicMeasure<decimal>(4, 5, 6),
        new TriPhasicMeasure<decimal>(5, 7, 9)
      ),
      (
        new NullPhasicMeasure<decimal>(),
        new SinglePhasicSumMeasure<decimal>(2),
        new NullPhasicMeasure<decimal>()
      ),
    };
  }

  [Test]
  [MethodDataSource(nameof(PhasicMeasuresAdd))]
  public void AddReturnsExpectedResult(
    PhasicMeasure<decimal> lhs,
    PhasicMeasure<decimal> rhs,
    PhasicMeasure<decimal> expected
  )
  {
    var result = lhs.Add(rhs);

    result.Should().Be(expected);
  }
}
