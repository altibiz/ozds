using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexSumTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>>
    DuplexMeasuresSum = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3)),
        new SinglePhasicMeasure<decimal>(8)
      },
      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)),
        new TriPhasicMeasure<decimal>(5, 7, 9)
      },
      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(4)),
        new SinglePhasicMeasure<decimal>(4)
      },
      {
        new AnyDuplexMeasure<decimal>(new SinglePhasicMeasure<decimal>(3)),
        new SinglePhasicMeasure<decimal>(3)
      },
      { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresSum))]
  public void DuplexSumReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexSum;

    Assert.Equal(expected, result);
  }
}
