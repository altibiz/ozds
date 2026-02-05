using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffBinaryTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    BinaryTariffMeasure<decimal>
  )> TariffMeasuresBinary()
  {
    return new List<(TariffMeasure<decimal>, BinaryTariffMeasure<decimal>)>
    {
      (
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1),
            new SinglePhasicSumMeasure<decimal>(2)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(4)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1),
            new SinglePhasicSumMeasure<decimal>(2)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(4)
          )
        )
      ),
      (
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(7, 8, 9),
            new TriPhasicMeasure<decimal>(10, 11, 12)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(7, 8, 9),
            new TriPhasicMeasure<decimal>(10, 11, 12)
          )
        )
      ),
      (
        new NullTariffMeasure<decimal>(),
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          DuplexMeasure<decimal>.Null
        )
      ),
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresBinary))]
  public void TariffBinary_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    BinaryTariffMeasure<decimal> expected
  )
  {
    var result = measure.TariffBinary();

    result.Should().Be(expected);
  }
}
