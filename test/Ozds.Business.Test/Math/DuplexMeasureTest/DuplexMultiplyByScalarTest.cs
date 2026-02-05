using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexMultiplyByScalarTest
{
  public static IEnumerable<(
    DuplexMeasure<decimal>,
    decimal,
    DuplexMeasure<decimal>
  )> DuplexMeasuresMultiply()
  {
    return new List<(DuplexMeasure<decimal>, decimal, DuplexMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)
        ),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(10),
          new SinglePhasicSumMeasure<decimal>(6)
        )
      ),
      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)
        ),
        2,
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 4, 6),
          new TriPhasicMeasure<decimal>(8, 10, 12)
        )
      ),
      (
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4)),
        3,
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(12))
      ),
      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(3)),
        4,
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(12))
      ),
      (new NullDuplexMeasure<decimal>(), 2, new NullDuplexMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresMultiply))]
  public void MultiplyReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    decimal multiplier,
    DuplexMeasure<decimal> expected
  )
  {
    var result = measure.Multiply(multiplier);

    result.Should().Be(expected);
  }
}
