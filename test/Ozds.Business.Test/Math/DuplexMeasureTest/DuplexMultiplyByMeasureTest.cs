using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexMultiplyByMeasureTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, DuplexMeasure<decimal>,
      DuplexMeasure<decimal>> DuplexMeasuresMultiply = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(2),
          new SinglePhasicMeasureSum<decimal>(1)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(10),
          new SinglePhasicMeasureSum<decimal>(3))
      },

      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 3, 4),
          new TriPhasicMeasure<decimal>(1, 2, 3)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 6, 12),
          new TriPhasicMeasure<decimal>(4, 10, 18))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(12))
      },

      {
        new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>(),
        new NullDuplexMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresMultiply))]
  public void Multiply_ReturnsExpectedResult(
    DuplexMeasure<decimal> lhs,
    DuplexMeasure<decimal> rhs,
    DuplexMeasure<decimal> expected)
  {
    var result = lhs.Multiply(rhs);

    Assert.Equal(expected, result);
  }
}
