using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexExportTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>>
    DuplexMeasuresExport = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3)),
        new SinglePhasicMeasure<decimal>(3)
      },
      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(6, 8, 10),
          new TriPhasicMeasure<decimal>(1, 2, 3)),
        new TriPhasicMeasure<decimal>(1, 2, 3)
      },
      { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresExport))]
  public void DuplexExportReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexExport;

    Assert.Equal(expected, result);
  }
}
