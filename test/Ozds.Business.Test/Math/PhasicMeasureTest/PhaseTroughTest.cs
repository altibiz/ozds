using Xunit;
using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.PhasicMeasureTest;

public class PhaseTroughTest
{
  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresTroughSix = new()
  {
    { new SinglePhasicMeasure<decimal>(6) },
    { new TriPhasicMeasure<decimal>(100, 6.1m, 6) },
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresTroughSix))]
  public void ReturnsTroughSix(PhasicMeasure<decimal> x)
  {
    Assert.Equal(6, x.PhaseTrough);
  }

  public static readonly TheoryData<PhasicMeasure<decimal>> PhasicMeasuresTroughZero = new()
  {
    { new SinglePhasicMeasure<decimal>(0) },
    { new TriPhasicMeasure<decimal>(0, 0, 0) },
    { new NullPhasicMeasure<decimal>() }
  };

  [Theory]
  [MemberData(nameof(PhasicMeasuresTroughZero))]
  public void ReturnsTroughZero(PhasicMeasure<decimal> x)
  {
    Assert.Equal(0, x.PhaseTrough);
  }
}
