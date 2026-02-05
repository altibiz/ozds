using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexExportTest
{
  public static IEnumerable<(
    DuplexMeasure<decimal>,
    PhasicMeasure<decimal>
  )> DuplexMeasuresExport()
  {
    return new List<(DuplexMeasure<decimal>, PhasicMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(5),
          new SinglePhasicSumMeasure<decimal>(3)
        ),
        new SinglePhasicSumMeasure<decimal>(3)
      ),
      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(6, 8, 10),
          new TriPhasicMeasure<decimal>(1, 2, 3)
        ),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      ),
      (new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresExport))]
  public void DuplexExportReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected
  )
  {
    var result = measure.DuplexExport();

    result.Should().Be(expected);
  }
}
