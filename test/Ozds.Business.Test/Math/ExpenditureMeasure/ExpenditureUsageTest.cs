using Ozds.Business.Math;
using Xunit;

namespace Ozds.Business.Test.Math.ExpenditureMeasureTest;

public class ExpenditureUsageTest
{
  public static readonly
    TheoryData<ExpenditureMeasure<decimal>, TariffMeasure<decimal>>
    ExpenditureMeasuresUsage = new()
    {
      {
        new UsageExpenditureMeasure<decimal>(new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(5),
            new SinglePhasicMeasure<decimal>(3)))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(5),
          new SinglePhasicMeasure<decimal>(3)))
      },

      {
        new UsageExpenditureMeasure<decimal>(new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new TriPhasicMeasure<decimal>(1, 2, 3),
            new TriPhasicMeasure<decimal>(4, 5, 6)))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new TriPhasicMeasure<decimal>(1, 2, 3),
          new TriPhasicMeasure<decimal>(4, 5, 6)))
      },

      {
        new DualExpenditureMeasure<decimal>(
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(1),
              new SinglePhasicMeasure<decimal>(2))),
          new UnaryTariffMeasure<decimal>(
            new ImportExportDuplexMeasure<decimal>(
              new SinglePhasicMeasure<decimal>(3),
              new SinglePhasicMeasure<decimal>(4)))),
        new UnaryTariffMeasure<decimal>(new ImportExportDuplexMeasure<decimal>(
          new SinglePhasicMeasure<decimal>(1),
          new SinglePhasicMeasure<decimal>(2)))
      },

      {
        new NullExpenditureMeasure<decimal>(), new NullTariffMeasure<decimal>()
      }
    };

  [Theory]
  [MemberData(nameof(ExpenditureMeasuresUsage))]
  public void ExpenditureUsageReturnsExpectedResult(
    ExpenditureMeasure<decimal> measure, TariffMeasure<decimal> expected)
  {
    var result = measure.ExpenditureUsage;

    Assert.Equal(expected, result);
  }
}
