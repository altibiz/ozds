using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAbsTest
{
  public static IEnumerable<(DuplexMeasure<decimal>, DuplexMeasure<decimal>)>
    DuplexMeasuresAbs()
  {
    return new List<(DuplexMeasure<decimal>, DuplexMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(-5),
          new SinglePhasicMeasureSum<decimal>(-3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3))
      ),
      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(-1, -2, -3),
          new TriPhasicMeasure<decimal>(-4, -5, -6)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6))
      ),
      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(-2)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(2))
      ),
      (new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>())
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresAbs))]
  public void DuplexAbsReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    DuplexMeasure<decimal> expected)
  {
    var result = measure.DuplexAbs();

    result.Should().Be(expected);
  }
}
