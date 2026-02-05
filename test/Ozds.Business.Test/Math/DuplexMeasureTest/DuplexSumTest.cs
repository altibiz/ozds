using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexSumTest
{
  public static IEnumerable<(
    DuplexMeasure<decimal>,
    PhasicMeasure<decimal>
  )> DuplexMeasuresSum()
  {
    return new List<(DuplexMeasure<decimal>, PhasicMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)
        ),
        new SinglePhasicSumMeasure<decimal>(8)
      ),
      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)
        ),
        new TriPhasicMeasure<decimal>(5, 7, 9)
      ),
      (
        new NetDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(4)),
        new SinglePhasicSumMeasure<decimal>(4)
      ),
      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicSumMeasure<decimal>(3)),
        new SinglePhasicSumMeasure<decimal>(3)
      ),
      (new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresSum))]
  public void DuplexSumReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected
  )
  {
    var result = measure.DuplexSum();

    result.Should().Be(expected);
  }
}
