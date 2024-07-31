using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseMultiplyTest
{
  public static readonly
    TheoryData<PhasicMeasure<decimal>, decimal, PhasicMeasure<decimal>>
    PhasicMeasuresMultiply = new()
    {
      {
        new SinglePhasicMeasureSum<decimal>(5), 2,
        new SinglePhasicMeasureSum<decimal>(10)
      },

      {
        new TriPhasicMeasure<decimal>(1, 2, 3), 3,
        new TriPhasicMeasure<decimal>(3, 6, 9)
      },

      { new NullPhasicMeasure<decimal>(), 2, new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(PhasicMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    PhasicMeasure<decimal> measure,
    decimal multiplier,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.Multiply(multiplier);

    Assert.Equal(expected, result);
  }
}
