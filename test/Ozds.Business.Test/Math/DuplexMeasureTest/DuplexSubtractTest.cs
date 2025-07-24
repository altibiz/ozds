using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexSubtractTest
{
  public static IEnumerable<(DuplexMeasure<decimal>, DuplexMeasure<decimal>,
    DuplexMeasure<decimal>)> DuplexMeasuresSubtract()
  {
    return new List<(DuplexMeasure<decimal>, DuplexMeasure<decimal>,
      DuplexMeasure<decimal>)>
    {
      (new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(2),
          new SinglePhasicSumMeasure<decimal>(1)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(3),
          new SinglePhasicSumMeasure<decimal>(2))
      ),

      (new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(10, 9, 8),
          new TriPhasicMeasure<decimal>(5, 4, 3)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 1, 1),
          new TriPhasicMeasure<decimal>(1, 1, 1)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(9, 8, 7),
          new TriPhasicMeasure<decimal>(4, 3, 2))
      ),

      (new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(10)),
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4)),
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(6))
      ),

      (new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(7)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(3)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4))
      ),

      (new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>(),
        new NullDuplexMeasure<decimal>()
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresSubtract))]
  public void Subtract_ReturnsExpectedResult(
    DuplexMeasure<decimal> lhs,
    DuplexMeasure<decimal> rhs,
    DuplexMeasure<decimal> expected)
  {
    var result = lhs.Subtract(rhs);

    result.Should().Be(expected);
  }
}
