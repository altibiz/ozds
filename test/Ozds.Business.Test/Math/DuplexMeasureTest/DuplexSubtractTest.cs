using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexSubtractTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, DuplexMeasure<decimal>,
      DuplexMeasure<decimal>> DuplexMeasuresSubtract = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(2),
          new SinglePhasicMeasureSum<decimal>(1)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(3),
          new SinglePhasicMeasureSum<decimal>(2))
      },

      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(10, 9, 8),
          new TriPhasicMeasure<decimal>(5, 4, 3)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 1, 1),
          new TriPhasicMeasure<decimal>(1, 1, 1)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(9, 8, 7),
          new TriPhasicMeasure<decimal>(4, 3, 2))
      },

      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(10)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(6))
      },

      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(7)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4))
      },

      {
        new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>(),
        new NullDuplexMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresSubtract))]
  public void Subtract_ReturnsExpectedResult(
    DuplexMeasure<decimal> lhs,
    DuplexMeasure<decimal> rhs,
    DuplexMeasure<decimal> expected)
  {
    var result = lhs.Subtract(rhs);

    Assert.Equal(expected, result);
  }
}
