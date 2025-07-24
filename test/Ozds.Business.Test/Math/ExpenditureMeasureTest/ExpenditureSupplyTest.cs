using Ozds.Business.Math;

namespace Ozds.Business.Test.Math.ExpenditureMeasureTest;

public class ExpenditureSupplyTest
{
  public static
    IEnumerable<(ExpenditureMeasure<decimal>, TariffMeasure<decimal>)>
    ExpenditureMeasuresSupply()
  {
    return new List<(ExpenditureMeasure<decimal>, TariffMeasure<decimal>)>
    {
      (new SupplyExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicSumMeasure<decimal>(5),
              new SinglePhasicSumMeasure<decimal>(3)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicSumMeasure<decimal>(5),
            new SinglePhasicSumMeasure<decimal>(3)))
      ),

      (new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(7, 8, 9),
              new TriPhasicMeasure<decimal>(10, 11, 12)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(7, 8, 9),
            new TriPhasicMeasure<decimal>(10, 11, 12)))
      ),

      (new NullExpenditureMeasure<decimal>(), new NullTariffMeasure<decimal>()
      )
    };
  }

  [Test]
  [MethodDataSource(nameof(ExpenditureMeasuresSupply))]
  public void ExpenditureSupply_ReturnsExpectedResult(
    ExpenditureMeasure<decimal> measure,
    TariffMeasure<decimal> expected)
  {
    var result = measure.ExpenditureSupply();

    result.Should().Be(expected);
  }
}
