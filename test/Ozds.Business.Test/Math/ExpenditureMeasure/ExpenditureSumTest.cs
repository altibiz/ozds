using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.ExpenditureMeasureTest;

public class ExpenditureSumTest
{
  public static readonly
    TheoryData<ExpenditureMeasure<decimal>, TariffMeasure<decimal>>
    ExpenditureMeasuresSum = new()
    {
      {
        new UsageExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(5),
              new SinglePhasicMeasureSum<decimal>(3)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(5),
            new SinglePhasicMeasureSum<decimal>(3)))
      },

      {
        new SupplyExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new TriPhasicMeasure<decimal>(1, 2, 3),
              new TriPhasicMeasure<decimal>(4, 5, 6)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)))
      },

      {
        new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(1),
              new SinglePhasicMeasureSum<decimal>(2))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasureSum<decimal>(3),
              new SinglePhasicMeasureSum<decimal>(4)))),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasureSum<decimal>(4),
            new SinglePhasicMeasureSum<decimal>(6)))
      },

      {
        new NullExpenditureMeasure<decimal>(), new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(ExpenditureMeasuresSum))]
  public void ExpenditureSum_ReturnsExpectedResult(
    ExpenditureMeasure<decimal> measure,
    TariffMeasure<decimal> expected)
  {
    var result = measure.ExpenditureSum;

    Assert.Equal(expected, result);
  }
}
