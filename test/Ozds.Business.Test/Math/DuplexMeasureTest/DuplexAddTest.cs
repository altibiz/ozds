using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexAddTest
{
  public static
    IEnumerable<(DuplexMeasure<decimal>, DuplexMeasure<decimal>,
      DuplexMeasure<decimal>)> DuplexMeasuresAdd()
  {
    return new List<(DuplexMeasure<decimal>, DuplexMeasure<decimal>,
      DuplexMeasure<decimal>)>
    {
      (
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(2),
          new SinglePhasicMeasureSum<decimal>(1)),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(7),
          new SinglePhasicMeasureSum<decimal>(4))
      ),

      (
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(2, 3, 4),
          new TriPhasicMeasure<decimal>(1, 1, 1)),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(3, 5, 7),
          new TriPhasicMeasure<decimal>(5, 6, 7))
      ),

      (
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(6)),
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(10))
      ),

      (
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(3)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(7))
      ),
      (
        new NullDuplexMeasure<decimal>(), new NullDuplexMeasure<decimal>(),
        new NullDuplexMeasure<decimal>()
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(DuplexMeasuresAdd))]
  public void AddReturnsExpectedResult(
    DuplexMeasure<decimal> lhs,
    DuplexMeasure<decimal> rhs,
    DuplexMeasure<decimal> expected)
  {
    var result = lhs.Add(rhs);

    result.Should().Be(expected);
  }
}
