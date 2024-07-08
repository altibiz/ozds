using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexMultiplyByScalarTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, decimal, DuplexMeasure<decimal>>
    DuplexMeasuresMultiply = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(10),
          new SinglePhasicMeasureSum<decimal>(6))
      },

      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 4, 6),
          new TriPhasicMeasure<decimal>(8, 10, 12))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        3,
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        4,
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12))
      },

      { new NullDuplexMeasure<decimal>(), 2, new NullDuplexMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    decimal multiplier,
    DuplexMeasure<decimal> expected)
  {
    var result = measure.Multiply(multiplier);

    Assert.Equal(expected, result);
  }
}
