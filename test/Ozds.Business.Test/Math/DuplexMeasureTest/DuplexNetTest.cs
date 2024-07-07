using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexNetTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>>
    DuplexMeasuresNet = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new SinglePhasicMeasureSum<decimal>(2)
      },
      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(6, 8, 10),
          new TriPhasicMeasure<decimal>(1, 2, 3)),
        new TriPhasicMeasure<decimal>(5, 6, 7)
      },
      {
        new NetDuplexMeasure<decimal>(new SinglePhasicMeasureSum<decimal>(4)),
        new SinglePhasicMeasureSum<decimal>(4)
      },
      { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresNet))]
  public void DuplexNetReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexNet;

    Assert.Equal(expected, result);
  }
}
