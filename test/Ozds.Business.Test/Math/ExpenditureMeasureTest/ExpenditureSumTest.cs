using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.ExpenditureMeasureTest;

public class ExpenditureSumTest
{
  public static IEnumerable<(
    ExpenditureMeasure<decimal>,
    TariffMeasure<decimal>
  )> ExpenditureMeasuresSum()
  {
    return new List<(ExpenditureMeasure<decimal>, TariffMeasure<decimal>)>
    {
      (
        new UsageExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(5),
              new SinglePhasicSumMeasure<decimal>(3)
            )
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)
          )
        )
      ),
      (
        new SupplyExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6)
            )
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)
          )
        )
      ),
      (
        new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(1),
              new SinglePhasicSumMeasure<decimal>(2)
            )
          ),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(3),
              new SinglePhasicSumMeasure<decimal>(4)
            )
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(4),
            new SinglePhasicSumMeasure<decimal>(6)
          )
        )
      ),
      (new NullExpenditureMeasure<decimal>(), new NullTariffMeasure<decimal>()),
    };
  }

  [Test]
  [MethodDataSource(nameof(ExpenditureMeasuresSum))]
  public void ExpenditureSum_ReturnsExpectedResult(
    ExpenditureMeasure<decimal> measure,
    TariffMeasure<decimal> expected
  )
  {
    var result = measure.ExpenditureSum();

    result.Should().Be(expected);
  }
}
