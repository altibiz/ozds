using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexDivideByScalarTest
{
  public static IEnumerable<(
    DuplexMeasure<decimal>,
    decimal,
    DuplexMeasure<decimal>
  )> DuplexMeasuresDivide()
  {
    return new List<(DuplexMeasure<decimal>, decimal, DuplexMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(10),
          new SinglePhasicSumMeasure<decimal>(6)
        ),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)
        )
      ),
      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(8, 6, 4),
          new TriPhasicMeasure<decimal>(2, 1, 0.5m)
        ),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(4, 3, 2),
          new TriPhasicMeasure<decimal>(1, 0.5m, 0.25m)
        )
      ),
      (
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(12)),
        3,
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4))
      ),
      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(12)),
        4,
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(3))
      ),
      (new NullDuplexMeasure<decimal>(), 2, new NullDuplexMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresDivide))]
  public void DivideReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    decimal divisor,
    DuplexMeasure<decimal> expected
  )
  {
    var result = measure.Divide(divisor);

    result.Should().Be(expected);
  }
}
