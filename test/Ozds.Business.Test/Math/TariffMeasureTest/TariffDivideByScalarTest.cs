using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.TariffMeasureTest;

public class TariffDivideByScalarTest
{
  public static IEnumerable<(
    TariffMeasure<decimal>,
    decimal,
    TariffMeasure<decimal>
  )> TariffMeasuresDivide()
  {
    return new List<(TariffMeasure<decimal>, decimal, TariffMeasure<decimal>)>
    {
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(10),
            new SinglePhasicSumMeasure<decimal>(6)
          )
        ),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)
          )
        )
      ),
      (
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(8, 6, 4),
            new TriPhasicMeasure<decimal>(2, 1, 0.5m)
          )
        ),
        2,
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(4, 3, 2),
            new TriPhasicMeasure<decimal>(1, 0.5m, 0.25m)
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
        2,
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
      (new NullTariffMeasure<decimal>(), 2, new NullTariffMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(TariffMeasuresDivide))]
  public void Divide_ReturnsExpectedResult(
    TariffMeasure<decimal> measure,
    decimal divisor,
    TariffMeasure<decimal> expected
  )
  {
    var result = measure.Divide(divisor);

    result.Should().Be(expected);
  }
}
