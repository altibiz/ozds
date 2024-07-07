using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexDivideByScalarTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, decimal, DuplexMeasure<decimal>>
    DuplexMeasuresDivide = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(10),
          new SinglePhasicMeasureSum<decimal>(6)),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3))
      },

      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(8, 6, 4),
          new TriPhasicMeasure<decimal>(2, 1, 0.5m)),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(4, 3, 2),
          new TriPhasicMeasure<decimal>(1, 0.5m, 0.25m))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12)), 3,
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12)), 4,
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3))
      },

      { new NullDuplexMeasure<decimal>(), 2, new NullDuplexMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresDivide))]
  public void DivideReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    decimal divisor,
    DuplexMeasure<decimal> expected)
  {
    var result = measure.Divide(divisor);

    Assert.Equal(expected, result);
  }
}
