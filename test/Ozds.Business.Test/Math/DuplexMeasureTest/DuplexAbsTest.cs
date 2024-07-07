using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAbsTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, DuplexMeasure<decimal>>
    DuplexMeasuresAbs = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(-5),
          new SinglePhasicMeasureSum<decimal>(-3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3))
      },
      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(-1, -2, -3),
          new TriPhasicMeasure<decimal>(-4, -5, -6)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6))
      },
      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(-2)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(2))
      },
      { new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresAbs))]
  public void DuplexAbsReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    DuplexMeasure<decimal> expected)
  {
    var result = measure.DuplexAbs;

    Assert.Equal(expected, result);
  }
}
