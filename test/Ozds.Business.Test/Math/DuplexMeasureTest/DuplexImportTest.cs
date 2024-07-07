using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.DuplexMeasureTest;

public class DuplexImportTest
{
  public static readonly
    TheoryData<DuplexMeasure<decimal>, PhasicMeasure<decimal>>
    DuplexMeasuresImport = new()
    {
      {
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasureSum<decimal>(5),
          new SinglePhasicMeasureSum<decimal>(3)),
        new SinglePhasicMeasureSum<decimal>(5)
      },
      {
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(6, 8, 10),
          new TriPhasicMeasure<decimal>(1, 2, 3)),
        new TriPhasicMeasure<decimal>(6, 8, 10)
      },
      { new NullDuplexMeasure<decimal>(), new NullPhasicMeasure<decimal>() }
    };

  [Theory]
  [MemberData(nameof(DuplexMeasuresImport))]
  public void DuplexImportReturnsExpectedResult(
    DuplexMeasure<decimal> measure,
    PhasicMeasure<decimal> expected)
  {
    var result = measure.DuplexImport;

    Assert.Equal(expected, result);
  }
}
