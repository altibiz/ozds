using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffSubtractTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    TariffMeasure<decimal>,
    TariffMeasure<decimal>
  )> TariffMeasuresSubtract()
  {
    return new List<(
      TariffMeasure<decimal>,
      TariffMeasure<decimal>,
      TariffMeasure<decimal>
    )>
    {
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(1)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(3),
            new SinglePhasicSumMeasure<decimal>(2)
          )
        )
      ),
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(10, 9, 8),
            new TriPhasicMeasure<decimal>(5, 4, 3)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 1, 1),
            new TriPhasicMeasure<decimal>(1, 1, 1)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(9, 8, 7),
            new TriPhasicMeasure<decimal>(4, 3, 2)
          )
        )
      ),
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
            new SinglePhasicSumMeasure<decimal>(0.5m),
            new SinglePhasicSumMeasure<decimal>(1)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1.5m),
            new SinglePhasicSumMeasure<decimal>(2)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(0.5m),
            new SinglePhasicSumMeasure<decimal>(1)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(1.5m),
            new SinglePhasicSumMeasure<decimal>(2)
          )
        )
      ),
      (
        new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>(),
        new NullTariffMeasure<decimal>()
      ),
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresSubtract))]
  public void Subtract_ReturnsExpectedResult(
    TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs,
    TariffMeasure<decimal> expected
  )
  {
    var result = lhs.Subtract(rhs);

    result.Should().Be(expected);
  }
}
