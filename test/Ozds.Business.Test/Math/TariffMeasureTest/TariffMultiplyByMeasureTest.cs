using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffMultiplyByMeasureTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    TariffMeasure<decimal>,
    TariffMeasure<decimal>
  )> TariffMeasuresMultiply()
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
            new SinglePhasicSumMeasure<decimal>(10),
            new SinglePhasicSumMeasure<decimal>(3)
          )
        )
      ),
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(2, 3, 4),
            new TriPhasicMeasure<decimal>(1, 2, 3)
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(2, 6, 12),
            new TriPhasicMeasure<decimal>(4, 10, 18)
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
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(3)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(4),
            new SinglePhasicSumMeasure<decimal>(5)
          )
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(2),
            new SinglePhasicSumMeasure<decimal>(6)
          ),
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(12),
            new SinglePhasicSumMeasure<decimal>(20)
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
  [MethodDataSource(nameof(TariffMeasuresMultiply))]
  public void Multiply_ReturnsExpectedResult(
    TariffMeasure<decimal> lhs,
    TariffMeasure<decimal> rhs,
    TariffMeasure<decimal> expected
  )
  {
    var result = lhs.Multiply(rhs);

    result.Should().Be(expected);
  }
}
