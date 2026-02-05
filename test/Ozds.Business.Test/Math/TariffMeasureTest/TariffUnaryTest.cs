using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffUnaryTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    DuplexMeasure<decimal>
  )> TariffMeasuresUnary()
  {
    return new List<(TariffMeasure<decimal>, DuplexMeasure<decimal>)>
    {
      (
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(1)
          )
        ),
        new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicSumMeasure<decimal>(7),
          new SinglePhasicSumMeasure<decimal>(4)
        )
      ),
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          )
        ),
        new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)
        )
      ),
      (new NullTariffMeasure<decimal>(), DuplexMeasure<decimal>.Null),
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresUnary))]
  public void TariffUnary_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    DuplexMeasure<decimal> expected
  )
  {
    var result = measure.TariffUnary();

    result.Should().Be(expected);
  }
}
